using UnityEngine;
using UnityEngine.UI;

public class GameComplete : MonoBehaviour {
    [SerializeField] private AudioSource _winAudio;
    [SerializeField] private AudioSource _mainAudio;
    [SerializeField] private GameObject _winObjects;
    [SerializeField] private GameObject _playObjects;
    [SerializeField] private Image _fadeImage;
    [SerializeField] private GhostLogbook _ghostLogbook;
    [SerializeField] private float FadeDuration = 1f;
    [SerializeField] private bool testWin;

    private bool starting;
    private bool switched;
    private bool completed;
    private float timer = 0;

    private void Update() {
        if (completed) return;

        bool force = false;
#if UNITY_EDITOR
        force = testWin;
#endif

        if (!starting && (force || _ghostLogbook.AllDiscovered())) {
            starting = true;
            _winAudio.volume = 0;
            _winAudio.Play();
        }

        if (!starting) return;
        float timePer;

        if (!switched) {
            if (timer < FadeDuration) {
                timer += Time.deltaTime;
                timePer = Mathf.Min(timer / FadeDuration, 1);
                SetFade(timePer);
                _mainAudio.volume = 1 - timePer;
                _winAudio.volume = timePer;
                return;
            }
            _mainAudio.Stop();
            _playObjects.SetActive(false);
            _winObjects.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            switched = true;
            timer = 0;
        }

        if (timer < FadeDuration) {
            timer += Time.deltaTime;
            timePer = Mathf.Min(timer / FadeDuration, 1);
            SetFade(1 - timePer);
            return;
        }
        completed = true;
    }

    private void SetFade(float a) {
        Color c = _fadeImage.color;
        c.a = a;
        _fadeImage.color = c;
    }
}