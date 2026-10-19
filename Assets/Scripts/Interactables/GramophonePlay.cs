using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GramophonePlay : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Outline _outline;
    public event Action<bool> OnGramophoneStateChange;
    private bool playing = false;

    void Update()
    {
        if (_outline.enabled) {
            if (Mouse.current.leftButton.wasReleasedThisFrame) {
                playing = !playing;
                OnGramophoneStateChange?.Invoke(playing);
                if (playing) {
                    _audioSource.Play();
                } else {
                    _audioSource.Pause();
                }
            }
        }
    }
}
