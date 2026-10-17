using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GhostNoiseLoop : MonoBehaviour {
    [SerializeField] private float MinDelay = 3f;
    [SerializeField] private float MaxDelay = 8f;

    private AudioSource audioSource;
    private float timer;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable() {
        timer = Random.Range(MinDelay, MaxDelay);
    }

    void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0f) {
            audioSource.Play();
            timer = Random.Range(MinDelay, MaxDelay);
        }
    }
}