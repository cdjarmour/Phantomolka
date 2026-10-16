using UnityEngine;

public class GhostFade : MonoBehaviour
{
    [SerializeField] private Color col;
    [SerializeField] private GhostVanish _ghostVanish;

    [SerializeField] private float FadeInTime;
    [Range(0, 1)][SerializeField] private float OpacityCap;

    private Material mat;
    private float fadeInTimer = 0;

    private void Awake() {
        mat = GetComponent<Renderer>().material;
    }

    private void OnEnable() {
        Color c = col;
        c.a = 0;
        mat.color = c;
        fadeInTimer = 0;
    }

    void Update()
    {
        Color c = col;
        if (fadeInTimer < FadeInTime) {
            fadeInTimer += Time.deltaTime;
            c.a = Mathf.Min(fadeInTimer / FadeInTime, 1) * OpacityCap;
            mat.color = c;
            return;
        }


        c.a = OpacityCap * (1 - Mathf.Min(_ghostVanish.GetTimerPercent(), 1));
        mat.color = c;
        

    }
}
