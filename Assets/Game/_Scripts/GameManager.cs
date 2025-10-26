using UnityEngine;
using UnityEngine.Serialization;

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

    private float _gameTime;
    [SerializeField]
    private float delayTime = 2f;
    [SerializeField]
    private float minDelayTime = 0.4f; // Minimum delay to prevent too fast gameplay
    [SerializeField]
    private float delayReduction = .1f;


    private void Start() {
      DisplayManager.Instance.SetRandomImage();
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
  }
}