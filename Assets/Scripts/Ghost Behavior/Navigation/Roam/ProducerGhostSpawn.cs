using UnityEngine;

public class ProducerGhostSpawn : MonoBehaviour {
    [SerializeField] private GramophonePlay _gp;
    [SerializeField] private GameObject _ghost;
    [SerializeField] private Transform _player;
    [SerializeField] private float distanceBehind = 1f;

    void Start() {
        _gp.OnGramophoneStateChange += GramophoneState;
    }

    void GramophoneState(bool playing) {
        if (playing) {
            Vector3 back = -_player.forward;
            back.y = 0f;
            back.Normalize();

            Vector3 pos = _player.position + back * distanceBehind;
            pos.y = _player.position.y;

            _ghost.transform.position = pos;
            _ghost.transform.rotation = Quaternion.LookRotation(-back);
            _ghost.SetActive(true);
        } else {
            _ghost.SetActive(false);
        }
    }
}