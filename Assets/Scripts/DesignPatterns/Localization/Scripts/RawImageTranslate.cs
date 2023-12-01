using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class RawImageTranslate : MonoBehaviour
{
    [SerializeField]
    private string Path;

    private RawImage component;

    private void Awake()
    {
        component = GetComponent<RawImage>();

        OnLocalizationChanged();

        LocalizationManager.Instance.OnLocalizationChanged += OnLocalizationChanged;
    }

    private void OnDestroy()
    {
        LocalizationManager.Instance.OnLocalizationChanged -= OnLocalizationChanged;
    }

    private void OnLocalizationChanged()
    {
        component.texture = LocalizationManager.Instance.TranslateAsset<Texture2D>(Path);
    }
}
