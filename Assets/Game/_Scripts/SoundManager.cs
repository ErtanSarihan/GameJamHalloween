using UnityEngine;

namespace Game._Scripts {
  public class SoundManager : MonoBehaviour {
    public static SoundManager Instance;

    [SerializeField]
    private AudioClip hammerOnHitSound;
    [SerializeField]
    private AudioClip gameOverSound;

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
      HammerHitController.OnHammerHit += OnHammerHit;
    }


    private void OnHammerHit() {
      AudioSource.PlayClipAtPoint(hammerOnHitSound, Vector3.zero);
    }

    private void OnDestroy() {
      HammerHitController.OnHammerHit -= OnHammerHit;
    }
  }
}