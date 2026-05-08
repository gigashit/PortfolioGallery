using UnityEngine;

public class Exhibit : MonoBehaviour
{
    public string exhibitTitle;
    public int exhibitIndex;

    [Header("Info")]
    public string header;
    public string title;
    public string description;

    [Header("Positions")]
    public Transform cameraPosition;
    public Transform playerPosition;

    private InputSystem_Actions inputActions;

    private bool canInteract = false;

    private Browser browserScript;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        browserScript = FindAnyObjectByType<Browser>();
    }

    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.Player.Interact.performed += ctx => InteractWithExhibit();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void InteractWithExhibit()
    {
        if (!canInteract) return;

        browserScript.currentExhibitIndex = exhibitIndex;

        browserScript.ShowExhibit();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Show interact HUD

        canInteract = true;
    }

    private void OnTriggerExit(Collider other)
    {
        // Hide interact HUD

        canInteract = false;
    }
}
