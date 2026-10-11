using UnityEngine;

[RequireComponent(typeof(CameraSubject))]
public class GhostVanish : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float maxDistance = 30f;
    [SerializeField] private float vanishDelay = 0f;
    private float unseenTimer;

    private CameraSubject subject;
    private bool hasBeenSeen;

    void Awake()
    {
        subject = GetComponent<CameraSubject>();
        if (playerCamera == null) playerCamera = Camera.main;
    }

    void Update()
    {
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
            gameObject.SetActive(false);
    }
}