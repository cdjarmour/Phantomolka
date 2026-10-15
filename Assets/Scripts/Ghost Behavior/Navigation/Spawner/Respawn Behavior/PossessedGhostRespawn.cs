using UnityEngine;

public class PossessedGhostRespawn : MonoBehaviour, IRespawnPosition {
    [SerializeField] private float HeightAbove;
    [SerializeField] LayerMask _ground;
    public void SetRespawnLocation(Vector3 spawnPosition) {
        Transform parent = transform.parent;
        parent.position = new Vector3(spawnPosition.x, spawnPosition.y, spawnPosition.z);

        if (Physics.Raycast(parent.position, Vector3.down, out RaycastHit hit, 10f, _ground)) {
            parent.position = hit.point + Vector3.up * HeightAbove;
        }
    }
}
