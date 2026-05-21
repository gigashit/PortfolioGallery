using TMPro;
using UnityEngine;

public class LocalizedTextComponent : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    [TextArea(1, 10)]
    [SerializeField] private string finnishText;
    [TextArea(1, 10)]
    [SerializeField] private string englishText;

    private SettingsManager settingsManager;

    private void OnValidate()
    {
        text = GetComponent<TMP_Text>();
        settingsManager = FindAnyObjectByType<SettingsManager>();
    }

    public void LocalizeLanguage()
    {
        if (settingsManager == null) { Debug.LogError("SettingsManager not found"); return; }

        if (settingsManager.selectedLanguage == Language.English)
        {
            text.text = englishText;
        }
        else
        {
            text.text = finnishText;
        }
    }
}
