#if UNITY_EDITOR
using System.Collections;
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.AI;

public class Roam : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;

    [SerializeField] private float Speed;

    [Range(0,1)][SerializeField] private float MinDistPercent;
    [Range(0, 1)][SerializeField] private float TurnBeforePercent;

    [SerializeField] private float Radius;
    [Range(0, 359)][SerializeField] private float MoveAngle;

    [SerializeField] private float IdleMaxTime = 10f;
    [SerializeField] private float IdleMinTime = 1f;

    [Range(0,1)][SerializeField] private float IdleChance = .25f;

    private bool isRoaming = false;
    private bool paused = false;
    private float pathDistance = -1;


#if UNITY_EDITOR
    void OnDrawGizmos() {
        Handles.color = Color.red;

        float direction = Mathf.Atan2(transform.forward.z, transform.forward.x) * Mathf.Rad2Deg;
        direction += MoveAngle / 2;
        direction *= Mathf.Deg2Rad;
        Vector3 ValidCone = new Vector3(Mathf.Cos(direction), 0, Mathf.Sin(direction));

        Handles.DrawSolidArc(transform.position, Vector3.up, ValidCone, MoveAngle, Radius);
    }
#endif

    private void Start() {
        _agent.speed = Speed;
    }
    private void OnDisable() {
        isRoaming = false;
    }

       
    void Update()
    {
        if (paused) {
            StartCoroutine(StartIdle());
            return;
        }
        if (!isRoaming) {
            NextDestination(transform.forward);
            isRoaming = true;
        } else {
            if (!_agent.pathPending && pathDistance < 0) {
                pathDistance = _agent.remainingDistance;
            }
            isRoaming = !ReachedDestination();
        }
    }

    private bool ReachedDestination() {
        if (!_agent.pathPending && pathDistance >= 0) {
            if (_agent.remainingDistance <= pathDistance * TurnBeforePercent) {
                return true;
            }
        }
        return false;
    }

    private IEnumerator StartIdle() {
        float idleTimer = Random.Range(IdleMinTime,IdleMaxTime);


        yield return new WaitForSeconds(idleTimer);
        paused = false;
    }

    public void NextDestination(Vector3 forward) {
        pathDistance = -1;
        float angle = Random.Range(-MoveAngle / 2, MoveAngle / 2);
        Vector3 direction = Quaternion.AngleAxis(angle, Vector3.up) * forward;
        float distance = Random.Range(Radius * MinDistPercent, Radius);

        Vector3 destination = transform.position + direction * distance;
        _agent.destination = destination;
    }
    public void PauseRoam() {
        paused = true;
        _agent.ResetPath();
        _agent.velocity = Vector3.zero;
        isRoaming = false;
    }

    public void UnpauseRoam() {
        paused = false;
    }

}
