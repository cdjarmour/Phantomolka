using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Spawner : MonoBehaviour {
    [SerializeField] GhostManager _ghostManager;
    [SerializeField] private float RespawnTimer = 5f;
    
    private void Start() {
        _ghostManager.OnGhostVanish += RespawnGhost;
    }

    private void RespawnGhost(GhostVanish gv) {
        StartCoroutine(RespawnGhostTimer(gv));
    }



    private IEnumerator RespawnGhostTimer(GhostVanish gv) {
        yield return new WaitForSeconds(RespawnTimer);
        Vector3 pos = Spawnpoint.All[Random.Range(0, Spawnpoint.All.Count)].GetSpawnPosition();
        Transform t = gv.gameObject.transform.parent;
        t.position = new Vector3(pos.x, t.position.y, pos.z);
        gv.ResetState();
    }
}