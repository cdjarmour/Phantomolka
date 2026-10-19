using UnityEngine;

public class PlayerInvisibility : MonoBehaviour
{
    [SerializeField] private float HideRange = 1.0f;
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _ghostModel;

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(_ghostModel.transform.position, HideRange);
    }

    private void Update() {
        if (Vector3.Distance(_ghostModel.transform.position, _player.position) < HideRange) {
            _ghostModel.SetActive(false);
        } else {
            _ghostModel.SetActive(true);
        }
    }
}
