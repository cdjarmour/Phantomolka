using UnityEngine;
using UnityEngine.InputSystem;

public class PlacePumpkin : MonoBehaviour
{
    [SerializeField] GameObject _pumpkin;
    [SerializeField] Outline _outline;
    [SerializeField] QuestProgression _progression;
    [SerializeField] private GameObject _chefGhost;


    private void Update() {

        if (_progression.PlacedPumpkin || !_progression.Pumpkin) return;
        if (_outline.enabled) {
            if (Mouse.current.leftButton.wasReleasedThisFrame) {
                _pumpkin.SetActive(true);
                _chefGhost.SetActive(true);
            }
        }
    }
}
