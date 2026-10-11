using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] Rigidbody _rb;

    private InputAction moveInput;

    private void Start() {
        moveInput = InputSystem.actions.FindAction("Move"); 
    }


    private void FixedUpdate() {
        Vector2 input = moveInput.ReadValue<Vector2>().normalized;
        Vector3 dir = transform.right * input.x + transform.forward * input.y;
        _rb.linearVelocity = new Vector3(dir.x * speed, _rb.linearVelocity.y, dir.z * speed);
    }




}
