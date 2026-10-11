using UnityEngine;


public class HeldCamera : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float maxDistance = 30f;
    
    // Boolean tracking whether the camera is raised or not
    private bool isRaised;

    public bool IsRaised
    {
        get { return isRaised; }
    }

    public void SetRaised(bool raised)
    {
        isRaised = raised;
    }
    
    public void TakePhoto()
    {
        // Does nothing if the camera is not raised
        if (!isRaised) return;

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(playerCamera);
        Vector3 origin = playerCamera.transform.position;

        foreach (CameraSubject subject in CameraSubject.All)
        {
            // Skips if the subject is not visible to the player
            if (!subject.IsVisibleTo(playerCamera, maxDistance))
                continue;
            Debug.Log("Photo taken of: " + subject.SubjectName);
        }
    }
}
