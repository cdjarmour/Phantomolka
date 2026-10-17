using UnityEngine;

public class ScreenOverlayManager : MonoBehaviour {
    [SerializeField] private GameObject _ectoplasmOverlay;
    [SerializeField] private GameObject _hypnosisOverlay;
    [SerializeField] private float EctoplasmDuration = 3f;

    private float ectoplasmTimer = 0f;

    void Start() {
        _ectoplasmOverlay.SetActive(false);
        _hypnosisOverlay.SetActive(false);
    }

    void Update() {
        if (ectoplasmTimer > 0f) {
            ectoplasmTimer -= Time.deltaTime;
            if (ectoplasmTimer <= 0f)
                _ectoplasmOverlay.SetActive(false);
        }
    }

    public void EctoplasmEnable() {
        _ectoplasmOverlay.SetActive(true);
        ectoplasmTimer = EctoplasmDuration;
    }

    public void HypnosisDisable() {
        _hypnosisOverlay.SetActive(false);
    }

    public void HypnosisEnable() {
        _hypnosisOverlay.SetActive(true);
    }
}