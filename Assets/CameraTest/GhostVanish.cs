using UnityEngine;

[RequireComponent(typeof(CameraSubject))]
public class GhostVanish : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float maxDistance = 30f;
    [SerializeField] private float vanishDelay = 0.5f;
    [SerializeField] private GhostManager manager;

    private CameraSubject subject;
    private bool hasBeenSeen;
    private float unseenTimer;

    void Awake()
    {
        subject = GetComponent<CameraSubject>();
        if (playerCamera == null) playerCamera = Camera.main;
    }

    public void ResetState()
    {
        hasBeenSeen = false;
        unseenTimer = 0f;
        transform.parent.gameObject.SetActive(true);
    }

    void Update()
    {
        // Ghosts do not vanish from placedcamera views
        if (PlacedCamera.IsViewing && PlacedCamera.Current != null)
        {
            if (PlacedCamera.Current.Sees(subject))
                hasBeenSeen = true;

            unseenTimer = 0f;
            return;
        }

        if (subject.IsVisibleTo(playerCamera, maxDistance))
        {
            hasBeenSeen = true;
            unseenTimer = 0f;
            return;
        }

        if (!hasBeenSeen)
            return;
        
        unseenTimer += Time.deltaTime;
        if (unseenTimer >= vanishDelay)
        {
            transform.parent.gameObject.SetActive(false);
            manager.RemoveGhost(this);
        }
    }
}