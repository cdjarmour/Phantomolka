using UnityEngine;
using UnityEngine.InputSystem;

public class HeldCameraInput : MonoBehaviour
{
    [SerializeField] private HeldCamera heldCamera;
    [SerializeField] private InputActionReference raiseAction;
    [SerializeField] private InputActionReference takePhotoAction;

    void OnEnable()
    {
        raiseAction.action.Enable();
        takePhotoAction.action.Enable();

        // Camera gets raised when right click is held down, Photo taken with left click
        raiseAction.action.started += OnRaise;
        raiseAction.action.canceled += OnLower;
        takePhotoAction.action.performed += OnTakePhoto;
    }

    void OnDisable()
    {
        raiseAction.action.started -= OnRaise;
        raiseAction.action.canceled -=OnLower;
        takePhotoAction.action.performed -= OnTakePhoto;

        raiseAction.action.Disable();
        takePhotoAction.action.Disable();
    }

    void OnRaise(InputAction.CallbackContext context)
    {
        heldCamera.SetRaised(true);
    }

    void OnLower(InputAction.CallbackContext context)
    {
        heldCamera.SetRaised(false);
    }

    void OnTakePhoto(InputAction.CallbackContext context)
    {
        heldCamera.TakePhoto();
    }
}