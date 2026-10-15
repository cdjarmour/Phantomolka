using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
    [SerializeField] GhostManager _ghostManager;
    [SerializeField] private float RespawnTimer = 5f;
    
    private void Start() {
        _ghostManager.OnGhostVanish += RespawnGhost;
    }

    private void RespawnGhost(GameObject ghost) {
        StartCoroutine(RespawnGhostTimer(ghost));
    }



    private IEnumerator RespawnGhostTimer(GameObject ghost) {

        yield return new WaitForSeconds(RespawnTimer);
        List<Spawnpoint> validSpawns = new List<Spawnpoint>();
        CameraSubject ghostData = ghost.GetComponent<CameraSubject>();
        foreach (Spawnpoint sp in Spawnpoint.All) {
            if (sp.MeetsRequirements(ghostData.SpawnConditions)) {
                validSpawns.Add(sp);
            }
        }

        if (validSpawns.Count == 0) {
            Debug.Log("no spawn");
            yield break;
        }
        Debug.Log($"Respawning {ghost.name}, requires {ghostData.SpawnConditions.Count}: {string.Join(", ", ghostData.SpawnConditions)}");
        Vector3 pos = validSpawns[Random.Range(0, validSpawns.Count)].GetSpawnPosition();

        if (ghost.TryGetComponent(out IRespawnPosition rp)) {
            rp.SetRespawnLocation(pos);
        }
        if (ghost.TryGetComponent(out GhostVanish gv)) {
            gv.ResetState();
        }
        ghost.transform.parent.gameObject.SetActive(true);
        Debug.Log($"activeSelf: {ghost.activeSelf}, activeInHierarchy: {ghost.activeInHierarchy}, parent: {ghost.transform.parent?.name}");
    }
}