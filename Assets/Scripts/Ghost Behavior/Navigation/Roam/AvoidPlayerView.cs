using UnityEngine;
using UnityEngine.AI;

public class AvoidPlayerView : MonoBehaviour
{
    [SerializeField] private CameraSubject _cameraSubject;
    [SerializeField] private Camera _playerCam;
    [SerializeField] private Roam _roam;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float FleeSpeed;

    [SerializeField] private float DetectionDistance;

    private const float DISTANCE_OFFSET = 1.1f;

    private bool hasDestination = false;
    private bool destinationVisible = false;
    private Vector3 destination;

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(_cameraSubject.transform.position, DetectionDistance);
    }

    private void OnDisable() {
        if (hasDestination) {
            _roam.enableRoam();
            hasDestination = false;
            destinationVisible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_cameraSubject.IsVisibleTo(_playerCam, DetectionDistance)) {
            if (!hasDestination) {
                _roam.disableRoam();
                _agent.destination = GetFleeDestination();
                _agent.speed = FleeSpeed;
                hasDestination = true;

            }

            if (hasDestination) {

                Vector3 visualDestination = new Vector3(destination.x, _cameraSubject.transform.position.y, destination.z);
                Vector3 point = _playerCam.WorldToViewportPoint(visualDestination);
                bool onScreen = point.z > 0 && point.x > 0 && point.x < 1 && point.y > 0 && point.y < 1;
                if (onScreen) {
                    if (!Physics.Raycast(_playerCam.transform.position, visualDestination - _playerCam.transform.position, (visualDestination - _playerCam.transform.position).magnitude)) {
                        destinationVisible = true;
                    }
                }
            }

            if (destinationVisible) {
                _agent.destination = GetFleeDestination();
            }
            
        } else if (hasDestination) {
            _roam.enableRoam();
            hasDestination = false;
        }
    }

    private Vector3 GetFleeDestination() {
        Vector3 vec = transform.position - _playerCam.transform.position;
        Vector3 ortho = (new Vector3(vec.z, 0, -vec.x)).normalized;
        if (new Vector3(vec.x, 0, vec.z).sqrMagnitude < 0.25f) {
            Vector3 right = _playerCam.transform.right;
            ortho = new Vector3(right.x, 0, right.z).normalized;
        }

        Vector3 direction = ortho * (Vector3.Dot(_playerCam.transform.right, vec) >= 0 ? 1f : -1f);
        float dist = Mathf.Tan(Camera.VerticalToHorizontalFieldOfView(_playerCam.fieldOfView, _playerCam.aspect) / 2 * Mathf.Deg2Rad) * vec.magnitude;
        destination = transform.position + dist * DISTANCE_OFFSET * direction;
        destination = ClampToNavMesh(transform.position, destination, out bool clamped);
        if (clamped) {
            Vector3 opposite = transform.position - dist * DISTANCE_OFFSET * direction;
            opposite = ClampToNavMesh(transform.position, opposite, out bool oppositeClamped);

            if (!oppositeClamped || Vector3.Distance(transform.position, opposite) > Vector3.Distance(transform.position, destination)) {
                destination = opposite;
            }
        }
        destinationVisible = false;

        return destination;
    }

    private Vector3 ClampToNavMesh(Vector3 from, Vector3 target, out bool blocked) {
        blocked = NavMesh.Raycast(from, target, out NavMeshHit hit, NavMesh.AllAreas);
        if (!blocked) return target;

        Vector3 back = from - hit.position;
        back.y = 0;
        return hit.position + back.normalized * _agent.radius;
    }
}
