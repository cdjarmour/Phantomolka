using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] Rigidbody _rb;
    [SerializeField] private float JumpStrength;

    private InputAction moveInput;
    private InputAction jumpInput;

    private void Start() {
        moveInput = InputSystem.actions.FindAction("Move");
        jumpInput = InputSystem.actions.FindAction("Jump");
    }


    private void FixedUpdate() {
        Vector2 input = moveInput.ReadValue<Vector2>().normalized;
        Vector3 dir = transform.right * input.x + transform.forward * input.y;
        _rb.linearVelocity = new Vector3(dir.x * Speed, _rb.linearVelocity.y, dir.z * Speed);
    }

    private void Update() {
        if (jumpInput.WasPressedThisFrame()) {
            if (_rb.linearVelocity.y <= 0.1) {
                _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, JumpStrength, _rb.linearVelocity.z);
            }
        }
    }

}
