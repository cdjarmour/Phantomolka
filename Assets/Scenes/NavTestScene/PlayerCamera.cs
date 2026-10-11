using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour {

    public float sensitivity;

    public Transform _playerForward;

    float xRotation;
    float yRotation;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        float mouseX = mouseDelta.x * sensitivity;
        float mouseY = mouseDelta.y * sensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        _playerForward.rotation = Quaternion.Euler(0, yRotation, 0);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}