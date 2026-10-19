using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour {

    public float sensitivity;

    public Transform _playerForward;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        float mouseX = mouseDelta.x * sensitivity;
        float mouseY = mouseDelta.y * sensitivity;

        _playerForward.Rotate(0f, mouseX, 0f, Space.World);

        float pitch = transform.localEulerAngles.x;
        if (pitch > 180f) pitch -= 360f;
        pitch = Mathf.Clamp(pitch - mouseY, -90f, 90f);
        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}