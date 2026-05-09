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
    [SerializeField] private Button nextExhibitButton;
    [SerializeField] private Button previousExhibitButton;

    [Header("Browser UI Text Fields")]

    [SerializeField] private TMP_Text headerText;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;

    private Browser browserScript;

    [HideInInspector] public bool inMainMenu = true;

    private void Start()
    {
        browserScript = FindAnyObjectByType<Browser>();

        mainMenuParent.SetActive(true);
        playModeUIParent.SetActive(false);
        browseModeUIParent.SetActive(false);

        browserScript.isBrowserOn = true;
        inMainMenu = true;

        playModeButton.onClick.AddListener(StartPlayMode);
        browseModeButton.onClick.AddListener(StartBrowseMode);
        nextExhibitButton.onClick.AddListener(NextExhibit);
        previousExhibitButton.onClick.AddListener(PreviousExhibit);

        ToggleCursor(true);
    }

    public void StartPlayMode()
    {
        mainMenuParent.SetActive(false);
        browseModeUIParent.SetActive(false);

        FadeInUIElements(playModeUIParent);

        playModeUIParent.SetActive(true);

        ToggleCursor(false);

        browserScript.isBrowserOn = false;

        inMainMenu = false;
    }

    public void StartBrowseMode()
    {
        mainMenuParent.SetActive(false);
        playModeUIParent.SetActive(false);

        browseModeUIParent.SetActive(true);

        browserScript.ShowExhibit();

        ToggleCursor(true);

        inMainMenu = false;
    }

    private void NextExhibit()
    {
        if (browserScript.currentExhibitIndex == browserScript.exhibits.Count - 1)
        {
            browserScript.currentExhibitIndex = 0;
        }
        else
        {
            browserScript.currentExhibitIndex++;
        }

        browserScript.ShowExhibit();
    }

    private void PreviousExhibit()
    {
        if (browserScript.currentExhibitIndex == 0)
        {
            browserScript.currentExhibitIndex = browserScript.exhibits.Count - 1;
        }
        else
        {
            browserScript.currentExhibitIndex--;
        }

        browserScript.ShowExhibit();
    }

    public void ShowExhibitUI(Exhibit exhibit)
    {
        headerText.text = exhibit.header;
        titleText.text = exhibit.title;
        descriptionText.text = exhibit.description;

        FadeInUIElements(browseModeUIParent);
    }

    private void ToggleCursor(bool show)
    {
        Cursor.visible = show;
        Cursor.lockState = show ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void FadeInUIElements(GameObject parent)
    {
        Debug.Log("Fading in UI elements for " + parent.name);

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
