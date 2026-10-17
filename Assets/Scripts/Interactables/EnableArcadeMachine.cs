using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EnableArcadeMachine : MonoBehaviour
{
    [SerializeField] private List<GameObject> _ghosts;
    [SerializeField] private Outline _outline;
    private bool on = false;
    void Update()
    {
        if (_outline.enabled) {
            if (Mouse.current.leftButton.wasReleasedThisFrame) {
                if (on) {
                    on = false;
                    foreach (GameObject ghost in _ghosts) {
                        ghost.SetActive(false);
                    }

                } else {
                    on = true;
                    foreach (GameObject ghost in _ghosts) {
                        ghost.SetActive(true);
                    }
                }
            }
        }
    }
}
