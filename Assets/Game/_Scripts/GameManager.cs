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

    private float _gameTime = 0f;
    private float _delayTime = 2f;
    private float _baseDelayTime = 2f;
    private float _minDelayTime = 0.4f; // Minimum delay to prevent too fast gameplay
    private int score = 0;
    private int lastMilestone = 0; // Track last 100-point milestone
    


    private void Start() {
      DisplayManager.Instance.SetRandomImage();
    }


    private void Update() {
      _gameTime += Time.deltaTime;

      if (_gameTime >= _delayTime) {
        _gameTime = 0f;
        // Debug.Log("Timer finished!");
        DisplayManager.Instance.SetRandomImage();
      }

    }

    public void AddScore(int points) {
      score += points;
      Debug.Log($"Score: {score}");

      // Check if we've crossed a new 100-point milestone
      int currentMilestone = score / 100;
      if (currentMilestone > lastMilestone) {
        lastMilestone = currentMilestone;
        UpdateDelayTime();
      }
    }

    private void UpdateDelayTime() {
      // Decrease delay by 0.2 seconds, but don't go below minimum
      _delayTime = Mathf.Max(_baseDelayTime - (lastMilestone * 0.2f), _minDelayTime);
      Debug.Log($"Delay time updated to: {_delayTime}s (Milestone: {lastMilestone})");
    }

    public int GetScore() {
      return score;
    }

    public float GetDelayTime() {
      return _delayTime;
    }
  }
}