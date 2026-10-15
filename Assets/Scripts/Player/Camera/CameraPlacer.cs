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
        
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (!Physics.Raycast(ray, out RaycastHit hit, reach, ~0, QueryTriggerInteraction.Ignore))
            return;
        
        if (placed != null)
            Destroy(placed.gameObject);
        
        // Keep camera upright and flat when placed
        Vector3 flat = Vector3.ProjectOnPlane(hit.normal, Vector3.up);
        if (flat.sqrMagnitude < 0.01f)
            flat = -playerCamera.transform.forward;

        placed = Instantiate(cameraPrefab, hit.point + hit.normal * 0.05f, Quaternion.LookRotation(flat));
    }
}