using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Roam))]
public class CollisionTurn : MonoBehaviour
{
    [SerializeField] private float CollisionDist;
    private Roam roam;

    private bool running = false;

    private void Start() {
        roam = GetComponent<Roam>();
    }


    private void Update() {

        bool collision = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, CollisionDist);
        if (collision && !running) {
            Debug.Log(hit.normal);
            Vector3 direction = Vector3.Reflect(transform.forward, hit.normal).normalized;
            running = true;
            roam.NextDestination(direction);
        } else if (running && !collision) {
            running = false;
        }
        Debug.DrawRay(transform.position, transform.forward * CollisionDist, Color.yellow);
    }
}
