using TMPro;
using UnityEngine;

namespace Game._Scripts {
  public class ScoreManager : MonoBehaviour {
    public static ScoreManager Instance { get; private set; }

    private int _score;

    [SerializeField]
    private TextMeshProUGUI scoreText;
    
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
      scoreText.text = _score.ToString();
      HammerHitController.OnGlitchingImageHit += OnGlitchingImageHit;
    }

    private void OnGlitchingImageHit() {
      _score += 100;
      scoreText.text = _score.ToString();
    }
  }
}