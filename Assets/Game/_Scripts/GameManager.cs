using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game._Scripts {
  public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    private void Awake() {
      if (Instance == null) {
        Instance = this;
        DontDestroyOnLoad(gameObject);
      }
      else {
        Destroy(gameObject);
      }
    }

    public static event Action OnGameStarted;
    public static event Action OnGameOver;
    public static event Action OnRestartGame;


    [SerializeField]
    private GameObject gameOverPanel;

    private float _gameTime;
    [SerializeField]
    private float delayTime = 2f;
    [SerializeField]
    private float minDelayTime = 0.4f; // Minimum delay to prevent too fast gameplay
    [SerializeField]
    private float delayReduction = .1f;

    private bool _waitingForRestart;

    private void OnEnable() {
      HammerHitController.OnNonGlitchingImageHit += EndGame;
      HammerHitController.OnRepeatingButtonsHit += EndGame;
      DisplayManager.OnGlitchMiss += EndGame;
    }

    private void Start() {
      OnGameStarted?.Invoke();
    }

    private void EndGame() {
      OnGameOver?.Invoke();
      Time.timeScale = 0f;
      gameOverPanel.SetActive(true);
      _waitingForRestart = true;
      Debug.Log("Game Ended");
    }


    private void Update() {
      if (_waitingForRestart) {
        if (Keyboard.current.fKey.wasPressedThisFrame) {
          RestartGame();
          return;
        }
      }

      _gameTime += Time.deltaTime;

      if (_gameTime >= delayTime) {
        _gameTime = 0f;
        // Debug.Log("Timer finished!");
        DisplayManager.Instance.SetRandomImage();
        if (delayTime > minDelayTime) {
          delayTime -= delayReduction;
        }
        else {
          delayTime = minDelayTime;
        }
      }
    }

    private void RestartGame() {
      OnRestartGame?.Invoke();
      Time.timeScale = 1f;
      _waitingForRestart = false;
      _gameTime = 0f;
      gameOverPanel.SetActive(false);
    }

    private void OnDisable() {
      DisplayManager.OnGlitchMiss -= EndGame;
      HammerHitController.OnNonGlitchingImageHit -= EndGame;
      HammerHitController.OnRepeatingButtonsHit -= EndGame;
    }

    public void QuitGame() {
      Debug.Log("Game Quit");
      Application.Quit();
      UnityEditor.EditorApplication.isPlaying = false;
    }
  }
}