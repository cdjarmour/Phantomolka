using UnityEngine;
using UnityEngine.AI;

public class GhostRoll : MonoBehaviour {
    [SerializeField] private float RollSpeed = 10f;
    [SerializeField] private Roam _roam;


    void Update() {
        if (!_roam.getRoaming()) return;

        transform.Rotate(Vector3.right, RollSpeed * Time.deltaTime, Space.Self);
    }
}