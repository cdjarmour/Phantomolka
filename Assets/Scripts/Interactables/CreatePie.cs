using UnityEngine;
using UnityEngine.InputSystem;

public class CreatePie : MonoBehaviour
{
    [SerializeField] GameObject _pumpkin;
    [SerializeField] GameObject _pie;
    [SerializeField] GameObject _chefGhost;
    [SerializeField] private QuestProgression _qp;
    [SerializeField] private Outline _outline;

    [SerializeField] private float TimeToTransition;
    private float timer = 0;
    private bool converting = false;


    void Update() {
        if (!(_qp.Pumpkin == false || _qp.PumpkinPie == true || _qp.Completed == true)) {
            if (_outline.enabled) {
                if (Mouse.current.leftButton.wasReleasedThisFrame) {
                    converting = true;
                    _qp.PumpkinPie = true;


                }
            }
        }

        if (!converting) return;

        if (timer < TimeToTransition) {
            timer += Time.deltaTime;
        }

    }




}
