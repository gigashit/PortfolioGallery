using System;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public Language selectedLanguage;

    [SerializeField] private Transform uiParent;

    private void Start()
    {
        SetLanguage(Language.English);
    }

    private void SetLanguage(Language language)
    {
        selectedLanguage = language;

        SetLocalizedText(uiParent);
    }

    private void SetLocalizedText(Transform parent)
    {
        foreach (Transform child in parent)
        {
            LocalizedTextComponent text = child.GetComponent<LocalizedTextComponent>();

            if (text != null)
            {
                text.LocalizeLanguage();
            }

            SetLocalizedText(child);
        }
    }
}

public enum Language
{
    Finnish,
    English
}
