using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPlacer : MonoBehaviour
{
    [SerializeField] private InputActionReference placeAction;
    [SerializeField] private PlacedCamera cameraPrefab;
    [SerializeField] private float reach = 1f;

    private Camera playerCamera;
    private PlacedCamera placed;

    void Awake()
    {
        playerCamera = Camera.main;
    }

    void OnEnable()
    {
        placeAction.action.Enable();
        placeAction.action.performed += OnPlace;
    }

    void OnDisable()
    {
        placeAction.action.performed -= OnPlace;
        placeAction.action.Disable();
    }

    void OnPlace(InputAction.CallbackContext context)
    {
        if (PlacedCamera.IsViewing)
            return;

        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        forward = forward.normalized;

        Vector3 start = playerCamera.transform.position + forward * reach;
        if (!Physics.Raycast(start, Vector3.down, out RaycastHit hit, 5f))
            return;

        if (placed != null)
            Destroy(placed.gameObject);

        placed = Instantiate(cameraPrefab, hit.point, Quaternion.LookRotation(forward));
    }
}