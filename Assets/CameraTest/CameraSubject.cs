using UnityEngine;
using System.Collections.Generic;

public class CameraSubject : MonoBehaviour
{
    // Stores a list of all CameraSubjects that are enabled
    public static readonly List<CameraSubject> All = new List<CameraSubject>();

    [SerializeField] private string subjectName = "Ghost";
    
    private Renderer rend;

    public string SubjectName
    {
        get { return subjectName; }
    }

    public Bounds Bounds
    {
        get { return rend.bounds; }
    }

    public bool IsVisibleTo(Camera cam, float maxDistance)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
        // Stores the bounds of the subject
        Bounds bounds = Bounds;

        // Return false if the bounds are outside the frustum planes
        if (!GeometryUtility.TestPlanesAABB(planes, bounds))
            return false;
        
        Vector3 origin = cam.transform.position;
        Vector3 toTarget = bounds.center - origin;
        float distance = toTarget.magnitude;
        // Return false if the target is too far away from the player
        if (distance > maxDistance)
            return false;

        if (Physics.Raycast(origin, toTarget, out RaycastHit hit, distance, ~0, QueryTriggerInteraction.Ignore))
            return hit.transform == transform || hit.transform.IsChildOf(transform);

        return true;
    }

    // Renderer is used to get the bounds of the object
    void Awake()
    {
        rend = GetComponentInChildren<Renderer>();
    }

    void OnEnable()
    {
        All.Add(this);
    }

    void OnDisable()
    {
        All.Remove(this);
    }
}