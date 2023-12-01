using TMPro;

using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextTranslate : MonoBehaviour
{
    [SerializeField]
    private string Key;

    private TextMeshProUGUI component;

    private void Awake()
    {
        component = GetComponent<TextMeshProUGUI>();

        OnLocalizationChanged();

        LocalizationManager.Instance.OnLocalizationChanged += OnLocalizationChanged;
    }

    private void OnDestroy()
    {
        LocalizationManager.Instance.OnLocalizationChanged -= OnLocalizationChanged;
    }

    private void OnLocalizationChanged()
    {
        component.text = LocalizationManager.Instance.TranslateText(Key, "Rolando");
    }
}
