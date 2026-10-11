using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField] private float Radius;


    void OnDrawGizmos() {
        Handles.color = Color.green;
        Handles.DrawSolidDisc(transform.position, Vector3.up, Radius);
    }





}
