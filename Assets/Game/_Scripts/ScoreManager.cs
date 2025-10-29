using TMPro;
using UnityEngine;

namespace Game._Scripts {
  public class ScoreManager : MonoBehaviour {
    public static ScoreManager Instance { get; private set; }

    private int _score;
    private int _highScore;

    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI highScoreText;
    
    private void Awake() {
      if (Instance == null) {
        Instance = this;
        DontDestroyOnLoad(gameObject);
      }
      else {
        Destroy(gameObject);
      }
    }

    private void Start() {
      _score = 0;
      _highScore = PlayerPrefs.GetInt("HighScore", 0);


      UpdateScoreUI();
      HammerHitController.OnGlitchingImageHit += OnGlitchingImageHit;
      GameManager.OnRestartGame += ResetScore;
    }

    private void OnGlitchingImageHit() {
      _score += 100;

      if (_score > _highScore) {
        _highScore = _score;
        PlayerPrefs.SetInt("HighScore", _highScore);
        PlayerPrefs.Save();
      }
      UpdateScoreUI();
    }

    private void ResetScore() {
      _score = 0;

      UpdateScoreUI();
    }
    private void UpdateScoreUI() {
      scoreText.text = _score.ToString();
      if (highScoreText != null)
        highScoreText.text = _highScore.ToString();
    }
  }
}