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