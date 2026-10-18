using UnityEngine;

public class GhostRespawn : MonoBehaviour, IRespawnPosition
{
    public void SetRespawnLocation(Vector3 spawnPosition) {
        if (UnityEngine.AI.NavMesh.SamplePosition(spawnPosition, out UnityEngine.AI.NavMeshHit hit, 10f, UnityEngine.AI.NavMesh.AllAreas))
            spawnPosition = hit.position;
        transform.parent.position = spawnPosition;
    }
}
