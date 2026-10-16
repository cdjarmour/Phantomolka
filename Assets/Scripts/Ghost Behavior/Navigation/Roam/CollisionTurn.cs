using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Roam))]
public class CollisionTurn : MonoBehaviour
{
    [SerializeField] private float CollisionDist;
    [SerializeField] private float DetectionHeight;
    private Roam roam;

    private bool running = false;

    private void Start() {
        roam = GetComponent<Roam>();
    }


    private void Update() {
        Vector3 origin = transform.position + (Vector3.up * DetectionHeight);
        bool collision = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, CollisionDist);
        if (collision && !running) {
            Debug.Log(hit.normal);
            Vector3 direction = Vector3.Reflect(transform.forward, hit.normal).normalized;
            running = true;
            roam.NextDestination(direction);
        } else if (running && !collision) {
            running = false;
        }
        Debug.DrawRay(origin, transform.forward * CollisionDist, Color.yellow);
    }
}
