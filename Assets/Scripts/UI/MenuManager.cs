using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    [SerializeField] private GameObject _credits;
    [SerializeField] private GameObject _controls;

    [SerializeField] private Button _creditButton;
    [SerializeField] private Button _controlButton;
    [SerializeField] private Button _startButton;


    [SerializeField] private Image _fadeImage;
    [SerializeField] private float FadeDuration = 1f;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _playObjects;
    [SerializeField] private AudioSource _audioSource;

    private GameObject[] _tabs;
    private bool starting;
    private float timer = 0;

    void Awake() {
        _tabs = new[] { _credits, _controls };
    }

    void Start() {
        CloseAll();
        _creditButton.onClick.AddListener(() => Toggle(_credits));
        _controlButton.onClick.AddListener(() => Toggle(_controls));
        _startButton.onClick.AddListener(() => StartGame());
    }

    private void Toggle(GameObject panel) {
        bool open = panel.activeSelf;
        CloseAll();
        if (!open)
            panel.SetActive(true);
    }

    private void CloseAll() {
        foreach (GameObject tab in _tabs)
            tab.SetActive(false);
    }

    private void StartGame() {
        starting = true;
        _fadeImage.raycastTarget = true;
    }


    private void Update() {
        if (!starting) return;
        float timePer;
        if (timer < FadeDuration) {
            timer += Time.deltaTime;
            timePer = Mathf.Min(timer / FadeDuration, 1);
            Color c = _fadeImage.color;
            c.a = timePer;
            _fadeImage.color = c;
            _audioSource.volume = 1 - timePer;
            return;
        }
        _playObjects.SetActive(true);
        _mainMenu.SetActive(false);
    }


}