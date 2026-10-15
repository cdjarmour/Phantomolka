using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GhostRespawn : MonoBehaviour, IRespawnPosition
{
    public void SetRespawnLocation(Vector3 spawnPosition) {
        transform.parent.position = new Vector3(spawnPosition.x, transform.position.y, spawnPosition.z);
    }
}
