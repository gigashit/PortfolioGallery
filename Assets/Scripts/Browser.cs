using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Browser : MonoBehaviour
{
    [HideInInspector] public int currentExhibitIndex = 0;
    [HideInInspector] public bool isBrowserOn = true;

    public List<Exhibit> exhibits = new List<Exhibit>();

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cameraTransform;

    private InputSystem_Actions inputActions;

    private UIManager uiManager;

    private bool toggleDelay = false;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();

        uiManager = FindAnyObjectByType<UIManager>();
    }

    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.Player.Jump.performed += ctx => ToggleBrowserMode();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void ToggleBrowserMode()
    {
        if (toggleDelay || uiManager.inMainMenu) return;

        if (!isBrowserOn)
        {
            uiManager.StartBrowseMode();
        }
        else
        {
            uiManager.StartPlayMode();
            CloseExhibit();
        }

        StartCoroutine(ModeToggleDelay());
    }

    private IEnumerator ModeToggleDelay()
    {
        toggleDelay = true;

        yield return new WaitForSeconds(1f);

        toggleDelay = false;
    }

    public void ShowExhibit()
    {
        Debug.Log("Showing exhibit");

        isBrowserOn = true;

        Exhibit chosenExhibit = exhibits.Find(x => x.exhibitIndex == currentExhibitIndex);

        if (chosenExhibit == null) { Debug.LogError("Exhibit with index number " + currentExhibitIndex + " not found"); return; }

        // Disable player controls

        // Enable browse controls

        // Move camera and player to exhibit positions

        // Show exhibit UI

        playerTransform.DOMove(chosenExhibit.playerPosition.position, 0.7f).SetEase(Ease.InOutQuad);

        playerTransform.DORotate(chosenExhibit.playerPosition.rotation.eulerAngles, 0.7f).SetEase(Ease.InOutQuad);

        cameraTransform.DOMove(chosenExhibit.cameraPosition.position, 0.7f).SetEase(Ease.InOutQuad);

        cameraTransform.DORotate(chosenExhibit.cameraPosition.rotation.eulerAngles, 0.7f).SetEase(Ease.InOutQuad);

        uiManager.ShowExhibitUI(chosenExhibit);
    }

    public void CloseExhibit()
    {
        // Disable browse controls

        // Enable player controls

        // Return camera to player position

        isBrowserOn = false;
    }
}
