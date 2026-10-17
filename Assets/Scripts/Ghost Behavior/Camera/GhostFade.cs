using UnityEngine;

public class GhostFade : MonoBehaviour {
    [SerializeField] private GhostVanish _ghostVanish;
    [SerializeField] private float FadeInTime;
    [Range(0, 1)][SerializeField] private float OpacityCap = 1f;

    private Renderer[] renderers;
    private float fadeInTimer = 0;

    private void Awake() {
        renderers = GetComponentsInChildren<Renderer>(true);
    }

    private void OnEnable() {
        SetOpacity(0);
        fadeInTimer = 0;
    }

    void Update() {
        if (fadeInTimer < FadeInTime) {
            fadeInTimer += Time.deltaTime;
            SetOpacity(Mathf.Min(fadeInTimer / FadeInTime, 1) * OpacityCap);
            return;
        }
        SetOpacity(OpacityCap * (1 - Mathf.Min(_ghostVanish.GetTimerPercent(), 1)));
    }

    private void SetOpacity(float opacity) {
        Vector4 remap = new Vector4(0, Mathf.Lerp(0.75f, 1f, opacity), 0, 0);
        foreach (Renderer r in renderers) {
            r.material.SetVector("_Remap_Alpha", remap);
            r.material.SetFloat("_Opacity", opacity);
        }
    }
}