using UnityEngine;
using UnityEngine.InputSystem;

public class GuideManager : MonoBehaviour {
    [SerializeField] private GameObject _guide;
    [SerializeField] private Behaviour[] disableWhileOpen;

    private bool isOpen;

    void Start() {
        OpenGuide(true);
    }

    void Update() {
        if (!Keyboard.current.gKey.wasPressedThisFrame)
            return;

        if (PlacedCamera.IsViewing)
            return;

        OpenGuide(!isOpen);
    }

    private void OpenGuide(bool open) {
        isOpen = open;
        _guide.SetActive(open);

        Cursor.lockState = open ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = open;

        foreach (Behaviour b in disableWhileOpen)
            if (b != null) b.enabled = !open;
    }
}