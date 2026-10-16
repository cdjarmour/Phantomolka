using UnityEngine;

public class PlacedCamera : MonoBehaviour
{
    public static PlacedCamera Current { get; private set; }
    public static bool IsViewing { get; private set; }
    
    // Only used for frustum
    [SerializeField] private Camera lens;
    [SerializeField] private PlayerCamera look;
    [SerializeField] private float maxDistance = 100f;

    void Awake()
    {
        lens.enabled = false;
        look.enabled = false;
    }

    void OnEnable()
    {
        Current = this;
    }

    void OnDisable()
    {
        if (Current == this)
            Current = null;
        if (lens.enabled)
            IsViewing = false;
    }

    public void Enter()
    {
        lens.enabled = true;
        look.enabled = true;
        IsViewing = true;
    }

    public void Exit()
    {
        lens.enabled = false;
        look.enabled = false;
        IsViewing = false;
    }

    public bool Sees(CameraSubject subject)
    {
        return subject.IsVisibleTo(lens, maxDistance);
    }

    public void TakePhoto()
    {
        foreach (CameraSubject subject in CameraSubject.All)
        {
            // Skips if the subject is not visible to the player
            if (!Sees(subject))
                continue;
            Debug.Log("Photo taken of: " + subject.SubjectName);
            if (GhostLogbook.Instance != null)
                GhostLogbook.Instance.Register(subject.SubjectName);
        }
    }
}