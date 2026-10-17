using UnityEngine;

[RequireComponent(typeof(GramophonePlay))]
public class GramophonePulse : MonoBehaviour {
    [SerializeField] private Transform _target;
    [SerializeField] private float bpm = 115f;
    [SerializeField] private float subdivision = 1f;
    [SerializeField] private float beatOffset = 0f;
    [SerializeField] private float pulseSize = 1.15f;
    [SerializeField] private float returnSpeed = 5f;

    private GramophonePlay gramophone;
    private AudioSource audioSource;
    private Vector3 startSize;
    private bool playing = false;
    private int lastBeat = -1;

    void Start() {
        if (_target == null) _target = transform;
        startSize = _target.localScale;

        audioSource = GetComponent<AudioSource>();
        gramophone = GetComponent<GramophonePlay>();
        gramophone.OnGramophoneStateChange += SetPlaying;
    }

    void OnDestroy() {
        if (gramophone != null) gramophone.OnGramophoneStateChange -= SetPlaying;
    }

    private void SetPlaying(bool value) {
        playing = value;
    }

    void Update() {
        if (playing) {
            float beatLength = 60f / (bpm * subdivision);
            int beat = Mathf.FloorToInt(audioSource.time / beatLength + beatOffset * subdivision);

            if (beat != lastBeat) {
                lastBeat = beat;
                _target.localScale = startSize * pulseSize;
            }
        }
        _target.localScale = Vector3.Lerp(_target.localScale, startSize, Time.deltaTime * returnSpeed);
    }
}