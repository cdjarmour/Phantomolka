using Unity.VisualScripting;
using UnityEngine;

public class QueenGhostSpawn : MonoBehaviour
{
    [SerializeField] GameObject _queenModel;
    [SerializeField] GhostLogbook _ghostLogbook;

    private void Update() {
        if (_queenModel.activeSelf == true) return;

        if (_ghostLogbook.AllDiscoveredQueen()) {
            _queenModel.SetActive(true);
        }
    }


}
