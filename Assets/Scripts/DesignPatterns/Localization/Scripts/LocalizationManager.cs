using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.WSA;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance { get; private set; }

    [SerializeField]
    private string defaultLanguageFolder;

    [SerializeField]
    private string localizationFolders;

    [SerializeField]
    private string localizationFile;

    public event Action OnLocalizationChanged;

    private readonly Dictionary<string, LocalizationTable> localizationTables = new();
    private LocalizationTable currentLocalizationTable;
    private string currentLocalizationFolder;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Can only have one {nameof(LocalizationManager)}.");
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(Instance);

        SetLanguageAs(defaultLanguageFolder);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            SetLanguageAs("EN_US");
        if (Input.GetKeyDown(KeyCode.W))
            SetLanguageAs("ES_LA");
    }

    public void SetLanguageAs(string languageFolder)
    {
        if (currentLocalizationFolder == languageFolder)
            return;

        if (!localizationTables.TryGetValue(languageFolder, out LocalizationTable table))
        {
            table = new(localizationFolders, localizationFile, languageFolder);
            localizationTables.Add(languageFolder, table);
        }

        currentLocalizationFolder = languageFolder;
        currentLocalizationTable = table;

        OnLocalizationChanged?.Invoke();
    }

    public string TranslateText(string key) => currentLocalizationTable.TranslateText(key);

    public string TranslateText(string key, params object[] parameters) => currentLocalizationTable.TranslateText(key, parameters);

    public T TranslateAsset<T>(string path)
        where T : UnityEngine.Object
        => currentLocalizationTable.TranslateAsset<T>(path);

    private class LocalizationTable
    {
        // Extraemos el contenido de las l�neas.
        // Con esta expresi�n regular nos aseguramos que soporte escaping de comillas y punto y comas.
        // Por ejemplo la siguiente linea:
        // MY_KEY;"Some;Va""lue"
        // Es capturada como:
        // MY_KEY y "Some;Va""lue"
        // Y luego, lo procesamos para que quede:
        // MY_KEY y Some;Va"lue

        // Esta expresi�n parece dif�cil de entender pero en realidad no lo es:
        // ^((?:[^;]*)|(?:\".*\"));((?:[^;]*)|(?:\".*\"))$

        // ^    ((?: [^;]*)|(?:\".*\"))    ;    ((?:[^;]*)|(?:\".*\"))    $
        // |    |_____________________|    |    |____________________|    |
        // |    |                          |    |                         Indica que la expresi�n debe terminar tras consumir toda la cadena de texto.
        // |    |                          |    Este grupo de captura es id�ntico al primero.
        // |    |                          C�racter que usamos para separar columnas.
        // |    Grupo de captura, o sea, lo que queremos obtener.
        // Indica que debe empezar la expresi�n desde el inicio de la cadena de texto.

        // (    (?: [^;]*)|(?:\".*\")   )
        // |____________________________|
        // Todo lo que se encuentre entre estos par�ntesis cuenta como una "captura", o sea, texto que queremos leer.

        // (?:[^;]*)   |   (?:\".*\")
        //             Operaci�n "OR", la expresi�n va a intentar satisfacer la primera condici�n, y si no puede intena la segunda.

        // (?:  [^;]*   )
        // |__          _
        // Son como un par�ntesis de mat�matica, indican que todo lo que esta adentro es una sola expressi�n.
        // De esta forma, algo que en m�tematica ser�a (5+4) / (4*3)
        // Aqui usariamos (?:5+4) / (?:4*3)

        // [^   ;   ]   *
        // |_   |   _
        // |    |
        // |    C�racter no deseado.
        // Determina que cualquier car�cter que no sea uno de los que se encuentran adentro de esta expresi�n son v�lidos, si se satisface, se "consume" el c�racter.

        // *
        // Determina que la expresi�n anterior debe realizarse cuantas veces se pueda hasta que no se pueda satisfacer.

        // (?:  \".*\"  )
        // |__          |
        // Ya lo explicamos antes.

        // \"   .*  \"
        // |
        // El car�cter " es especial en Regex, por lo que para usar un " literal, hay que a�adir un \ al principio, o sea, se satisface si encuentra un ", y lo consume.

        // .
        // El . es satisfecho por cualquier c�racter, consumiendolo en el proceso.

        // .*
        // Como mencione antes, * altera la expresi�n anterior, por lo que consume cuantos car�cteres pueda.

        // Si quieren analizar las expresiones pueden probar en https://regex101.com/

        private static readonly Regex RowPattern = new("^((?:[^;]*)|(?:\".*\"));((?:[^;]*)|(?:\".*\"))$");

        private readonly string localizationFolders;
        private readonly string languageFolder;
        private readonly Dictionary<string, string> textTable = new();
        private readonly Dictionary<(string Path, Type Type), UnityEngine.Object> assetsTable = new();

        public LocalizationTable(string localizationFolders, string localizationFile, string languageFolder)
        {
            this.localizationFolders = localizationFolders;
            this.languageFolder = languageFolder;

            TextAsset asset = Resources.Load<TextAsset>($"{localizationFolders}{languageFolder}/{localizationFile}");

            // Dividimos el archivo en líneas.
            string[] lines = asset.text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            if (lines.Length == 0)
            {
                Debug.LogError($"Language at folder {languageFolder} has localization table which is empty.");
                return;
            }

            // i = 1 porque nos salteamos la primera fila que son los títulos.
            for (int i = 1; i < lines.Length; i++)
            {
                Match result = RowPattern.Match(lines[i]);
                if (!result.Success)
                {
                    Debug.LogError($"Line {i} at localization table of langauge at folder {languageFolder} has an invalid format.");
                    continue;
                }

                // El grupo 0 es la captura total, por lo que la salteamos.

                string key = result.Groups[1].Value;
                if (string.IsNullOrEmpty(key))
                {
                    Debug.LogError($"Line {i} at localization table of langauge at folder {languageFolder} has an empty key.");
                    continue;
                }

                if (!TryParse(key, out key))
                {
                    Debug.LogError($"Line {i} at localization table of langauge at folder {languageFolder} has an invalid key format.");
                    continue;
                }

                if (!TryParse(result.Groups[2].Value, out string value))
                {
                    Debug.LogError($"Line {i} at localization table of langauge at folder {languageFolder} has an invalid value format.");
                    continue;
                }

                if (!textTable.TryAdd(key, value))
                {
                    Debug.LogError($"Line {i} at localization table of langauge at folder {languageFolder} has a repeated key {key}.");
                    continue;
                }
            }

            static bool TryParse(string parse, out string result)
            {
                if (parse.StartsWith('"'))
                {
                    if (!parse.EndsWith('"'))
                    {
                        result = default;
                        return false;
                    }

                    // Removemos las comillas del inicio y del final.
                    string temp = parse.Substring(1, parse.Length - 1 - 1);
                    // Reemplazamos las dobles comillas por una sola.
                    result = temp.Replace("\"\"", "\"");
                    return true;
                }
                result = parse;
                return true;
            }
        }

        public string TranslateText(string key)
        {
            if (textTable.TryGetValue(key, out string value))
                return value;
            Debug.LogError($"Key {key} not found in language at folder {languageFolder}.");
            return key;
        }

        public string TranslateText(string key, params object[] parameters)
        {
            if (textTable.TryGetValue(key, out string value))
                return string.Format(value, parameters);
            Debug.LogError($"Key {key} not found in language at folder {languageFolder}.");
            return key;
        }

        public T TranslateAsset<T>(string path)
            where T : UnityEngine.Object
        {
            if (!assetsTable.TryGetValue((path, typeof(T)), out UnityEngine.Object value))
            {
                T obj = Resources.Load<T>($"{localizationFolders}{languageFolder}/{path}");
                assetsTable.Add((path, typeof(T)), obj);
                value = obj;
            }

            if (value is null)
            {
                Debug.LogError($"Path {path} not found in language at folder {languageFolder}.");
                return default;
            }

            return (T)value;
        }
    }
}
