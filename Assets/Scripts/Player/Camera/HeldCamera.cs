using UnityEngine;


public class HeldCamera : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float maxDistance = 100f;
    
    // Boolean tracking whether the camera is raised or not
    private bool isRaised;

    public bool IsRaised
    {
        get { return isRaised; }
    }

    public void SetRaised(bool raised)
    {
        if (PlacedCamera.IsViewing) raised = false;
        isRaised = raised;
    }
    
    public void TakePhoto()
    {
        // Does nothing if the camera is not raised
        if (!isRaised) return;

        foreach (CameraSubject subject in CameraSubject.All)
        {
            // Skips if the subject is not visible to the player
            if (subject.PlacedCameraOnly) continue;
            if (!subject.IsVisibleTo(playerCamera, maxDistance))
                continue;
            Debug.Log("Photo taken of: " + subject.SubjectName);
            if (GhostLogbook.Instance != null)
                GhostLogbook.Instance.Register(subject.SubjectName);
        }
    }
}
