using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] Rigidbody _rb;
    [SerializeField] private float JumpStrength;

    private InputAction moveInput;

    private void Start() {
        moveInput = InputSystem.actions.FindAction("Move");
    }

    private void OnDisable()
    {
        if (_rb != null)
            _rb.linearVelocity = new Vector3(0f, _rb.linearVelocity.y, 0f);
    }

    private void FixedUpdate() {
        Vector2 input = moveInput.ReadValue<Vector2>().normalized;
        Vector3 dir = transform.right * input.x + transform.forward * input.y;
        _rb.linearVelocity = new Vector3(dir.x * Speed, _rb.linearVelocity.y, dir.z * Speed);
    }


}
