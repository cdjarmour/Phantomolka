using UnityEngine;
using UnityEngine.AI;

public class PhotoBombNavigate : MonoBehaviour
{
    [SerializeField] private Roam _roam;
    [SerializeField] private Transform _player;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private HeldCamera _heldCamera;

    [SerializeField] private float FrontDistance;
    
    [SerializeField] private float UpdateFrequency;
    [SerializeField] private float DetectionDistance;

    [SerializeField] private float ChaseSpeed;

    private bool chasing = false;
    private float timer = 0f;

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, DetectionDistance);
    }


    private void OnDisable() {
        if (chasing) {
            chasing = false;
            _roam.enableRoam();
        }
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) <= DetectionDistance && _heldCamera.IsRaised && !chasing) {
            chasing = true;
            _roam.disableRoam();
            _agent.speed = ChaseSpeed;
            _agent.destination = GetPlayerFront();
        } else if ((Vector3.Distance(transform.position, _player.transform.position) > DetectionDistance || !_heldCamera.IsRaised) && chasing) {
            chasing = false;
            _roam.enableRoam();
        }

        if (chasing) {
            if (timer < UpdateFrequency) {
                timer += Time.deltaTime;
                return;
            }

            _agent.destination = GetPlayerFront();
            timer = 0;
        }

    }

    private Vector3 GetPlayerFront() {
        Vector3 frontPos = _player.transform.forward;
        frontPos.y = 0;
        frontPos = frontPos.normalized * FrontDistance;


        return _player.transform.position + frontPos;
    }


}
