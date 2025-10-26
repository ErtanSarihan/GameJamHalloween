using UnityEngine;

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

    [SerializeField]
    private GameObject gameOverPanel;

    private float _gameTime;
    [SerializeField]
    private float delayTime = 2f;
    [SerializeField]
    private float minDelayTime = 0.4f; // Minimum delay to prevent too fast gameplay
    [SerializeField]
    private float delayReduction = .1f;


    private void OnEnable() {
      HammerHitController.OnNonGlitchingImageHit += EndGame;
      DisplayManager.OnGlitchMiss += EndGame;
    }

    private void EndGame() {
      Time.timeScale = 0f;
      gameOverPanel.SetActive(true);
      Debug.Log("Game Ended");
    }


    private void Update() {
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

    private void OnDisable() {
      DisplayManager.OnGlitchMiss -= EndGame;
      HammerHitController.OnNonGlitchingImageHit -= EndGame;
    }
  }
}