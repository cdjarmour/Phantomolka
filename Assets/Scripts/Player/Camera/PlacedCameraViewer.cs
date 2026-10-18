using UnityEngine;
using UnityEngine.InputSystem;

public class PlacedCameraViewer : MonoBehaviour
{
    [SerializeField] private InputActionReference enterPlacedCameraAction;
    [SerializeField] private InputActionReference takePhotoAction;
    [SerializeField] private HeldCamera heldCamera; // Force held camera to be lowered when entering placed camera
    [SerializeField] private Behaviour[] disableWhileViewing;

    private Camera playerCamera;
    private PlacedCamera current;

    void Awake()
    {
        playerCamera = Camera.main;
    }

    void OnEnable()
    {
        enterPlacedCameraAction.action.Enable();
        takePhotoAction.action.Enable();
        enterPlacedCameraAction.action.performed += OnEnterPressed;
        takePhotoAction.action.performed += OnTakePhoto;
    }

    void OnDisable()
    {
        enterPlacedCameraAction.action.performed -= OnEnterPressed;
        takePhotoAction.action.performed -= OnTakePhoto;
        enterPlacedCameraAction.action.Disable();
    }

    void OnEnterPressed(InputAction.CallbackContext context)
    {
        if (current != null) 
        { 
            Exit(); 
            return;
        }

        PlacedCamera cam = PlacedCamera.Current;
        if (cam == null)
            return;
        Enter(cam);
    }

    void Enter(PlacedCamera cam)
    {
        current = cam;
        if (heldCamera != null)
            heldCamera.SetRaised(false);
        
        playerCamera.enabled = false;
        foreach (Behaviour b in disableWhileViewing)
            b.enabled = false;

        cam.Enter();
    }

    void Exit()
    {
        current.Exit();
        current = null;

        playerCamera.enabled = true;
        foreach (Behaviour b in disableWhileViewing)
            b.enabled = true;
    }

    void OnTakePhoto(InputAction.CallbackContext context)
    {
        if (current != null)
            current.TakePhoto();
    }
}