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
    private DifficultyManager difficultyManager;

    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private GameObject mainMenuPanel;

    private float _gameTime;
    [SerializeField]
    private float delayTime = 2f;

    private bool _waitingForRestart;

    private bool _isGameStart;

    private void OnEnable() {
      HammerHitController.OnNonGlitchingImageHit += EndGame;
      HammerHitController.OnRepeatingButtonsHit += EndGame;
      DisplayManager.OnGlitchMiss += EndGame;
    }

    private void StartGame() {
      OnGameStarted?.Invoke();
      Time.timeScale = 1f;
      _isGameStart = true;
      mainMenuPanel.SetActive(false);
      DisplayManager.Instance.SetRandomImage();
    }

    private void EndGame() {
      OnGameOver?.Invoke();
      Time.timeScale = 0f;
      gameOverPanel.SetActive(true);
      _waitingForRestart = true;
      Debug.Log("Game Ended");
    }


    private void Update() {
      if (!_isGameStart) {
        Time.timeScale = 0f;
        if (Keyboard.current.fKey.wasPressedThisFrame) {
          StartGame();
        }
      }
      else {
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
          delayTime = difficultyManager.GetCurrentDelay();
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