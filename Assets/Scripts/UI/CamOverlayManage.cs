using UnityEngine;

public class CamOverlay : MonoBehaviour {
    [SerializeField] private GameObject _overlay;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private HeldCamera _heldCamera;

    private void Update() {
        bool onMenu = _mainMenu != null && _mainMenu.activeInHierarchy;
        bool raised = _heldCamera != null && _heldCamera.isActiveAndEnabled && _heldCamera.IsRaised;
        bool show = onMenu || raised || PlacedCamera.IsViewing;

        if (_overlay.activeSelf != show)
            _overlay.SetActive(show);
    }
}