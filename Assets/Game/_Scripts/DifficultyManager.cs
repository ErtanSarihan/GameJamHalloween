using UnityEngine;

namespace Game._Scripts {
  public class DifficultyManager : MonoBehaviour {
    [SerializeField]
    private AnimationCurve imageDelayCurve;
    [SerializeField]
    private float maxDifficultyDuration = 70f;
    [SerializeField]
    private float initialDelay = 2f;
    [SerializeField]
    private float minimumDelay = 0.4f;

    private float _gameStartTime;

    private void OnEnable() {
      GameManager.OnGameStarted += OnGameStarted;
    }

    private void OnGameStarted() {
      _gameStartTime = Time.time;
    }


    public float GetCurrentDelay()
    {
      // Calculate how many seconds have passed since game started
      float elapsed = Time.time - _gameStartTime;
        
      // Convert elapsed time to a 0-1 range (normalized value)
      float t = Mathf.Clamp01(elapsed / maxDifficultyDuration);
        
      // Get the curve's Y value at position t
      float curveValue = imageDelayCurve.Evaluate(t);
        
      // Map curve value to actual delay time
      return Mathf.Lerp(initialDelay, minimumDelay, curveValue);
    }


    private void OnDisable() {
      GameManager.OnGameStarted -= OnGameStarted;
    }
  }
}