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
            // Stores the bounds of the subject
            Bounds bounds = subject.Bounds;

            // Continue if the bounds are outside the frustum planes
            if (!GeometryUtility.TestPlanesAABB(planes, bounds))
                continue;
            // Continue if the target is too far away from the player
            Vector3 toTarget = bounds.center - origin;
            float distance = toTarget.magnitude;
            if (distance > maxDistance)
                continue;

            if (Physics.Raycast(origin, toTarget, out RaycastHit hit, distance, ~0, QueryTriggerInteraction.Ignore))
            {
                bool hitIsSubject = hit.transform == subject.transform || hit.transform.IsChildOf(subject.transform);
                if (!hitIsSubject)
                    continue;
            }

            Debug.Log("Photo taken of: " + subject.SubjectName);
        }
    }
}
