using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Roam : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float Radius;

    private bool isRoaming = false;

    void OnDrawGizmos() {
        Handles.color = Color.red;
        Handles.DrawSolidDisc(transform.position, Vector3.up, Radius);
    }

    // Update is called once per frame
    void Update()
    {
         if (!isRoaming) {
            Vector2 point = Random.insideUnitCircle * Radius;
            Vector3 target = transform.position + new Vector3(point.x, 0, point.y);
            _agent.destination = target;
            isRoaming = true;
         } else {
            isRoaming = !ReachedDestination();
        }
    }



    private bool ReachedDestination() {
        if (!_agent.pathPending) {
            if (_agent.remainingDistance <= _agent.stoppingDistance) {
                if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f) {
                    return true;
                }
            }
        }
        return false;
    }
}
