using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CollectPumpkin : MonoBehaviour {
    [SerializeField] private QuestProgression _qp;
    [SerializeField] private Outline _outline;
    void Update() {
        if (_qp.Pumpkin == true) return;
        if (_outline.enabled) {
            if (Mouse.current.leftButton.wasReleasedThisFrame) {
                _qp.Pumpkin = true;
                Destroy(this.gameObject);
            }
        }
    }
}
