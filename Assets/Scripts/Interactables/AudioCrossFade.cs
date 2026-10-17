using UnityEditor;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(GramophonePlay))]
public class AudioCrossFade : MonoBehaviour
{
    [SerializeField] AudioSource _mainAudio;
    [SerializeField] Transform _player;
    [SerializeField] private float transitionSpeed;
    [Range(0,1)][SerializeField] private float MaxVolume;

    private GramophonePlay gramophonePlay;
    private bool playing = false;

    private AudioSource gramAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gramophonePlay = GetComponent<GramophonePlay>();
        gramophonePlay.OnGramophoneStateChange += SetState;
        gramAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        float step = Time.deltaTime / Mathf.Max(transitionSpeed, 0.0001f);
        bool inRange = Vector3.Distance(transform.position, _player.position) <= gramAudio.maxDistance;

        if (playing && inRange) {
            _mainAudio.volume = Mathf.MoveTowards(_mainAudio.volume, 0f, step);
            gramAudio.volume = Mathf.MoveTowards(gramAudio.volume, MaxVolume, step);
        } else {
            _mainAudio.volume = Mathf.MoveTowards(_mainAudio.volume, 1f, step);
            gramAudio.volume = Mathf.MoveTowards(gramAudio.volume, 0f, step);
        }
    }

    private void SetState(bool playing) {
        this.playing = playing;
        if (!playing)
            gramAudio.volume = 0f;
    }


}
