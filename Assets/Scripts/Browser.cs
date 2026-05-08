using System.Collections.Generic;
using UnityEngine;

public class Browser : MonoBehaviour
{
    [HideInInspector] public int currentExhibitIndex = 0;
    [HideInInspector] public bool isBrowserOn = true;

    public List<Exhibit> exhibits = new List<Exhibit>();

    private InputSystem_Actions inputActions;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
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

    }

    public void CloseExhibit()
    {
        // Disable browse controls

        // Enable player controls

        // Return camera to player position

        isBrowserOn = false;
    }
}
