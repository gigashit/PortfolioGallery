using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIManager : MonoBehaviour
{
    [Header("Menu Panels")]

    [SerializeField] private GameObject mainMenuParent;
    [SerializeField] private GameObject playModeUIParent;
    [SerializeField] private GameObject browseModeUIParent;

    [Header("Buttons")]

    [SerializeField] private Button playModeButton;
    [SerializeField] private Button browseModeButton;

    private Browser browserScript;

    private void Start()
    {
        browserScript = FindAnyObjectByType<Browser>();

        mainMenuParent.SetActive(true);
        playModeUIParent.SetActive(false);
        browseModeUIParent.SetActive(false);

        browserScript.isBrowserOn = true;

        playModeButton.onClick.AddListener(StartPlayMode);
        browseModeButton.onClick.AddListener(StartBrowseMode);

        ToggleCursor(true);
    }

    private void StartPlayMode()
    {
        mainMenuParent.SetActive(false);

        FadeInUIElements(playModeUIParent);

        playModeUIParent.SetActive(true);

        ToggleCursor(false);

        browserScript.isBrowserOn = false;
    }

    private void StartBrowseMode()
    {
        mainMenuParent.SetActive(false);

        FadeInUIElements(browseModeUIParent);

        browseModeUIParent.SetActive(true);

        browserScript.ShowExhibit();

        ToggleCursor(true);
    }

    private void ToggleCursor(bool show)
    {
        Cursor.visible = show;
        Cursor.lockState = show ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void FadeInUIElements(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            TMP_Text text = child.GetComponent<TMP_Text>();

            if (text != null )
            {
                Color originalColor = text.color;

                text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);

                text.DOFade(originalColor.a, 0.7f).SetEase(Ease.InOutQuad);
            }

            Image image = child.GetComponent<Image>();

            if (image != null )
            {
                Color originalColor = image.color;

                image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);

                image.DOFade(originalColor.a, 0.7f).SetEase(Ease.InOutQuad);
            }

            FadeInUIElements(child.gameObject);
        }
    }
}
