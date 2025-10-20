using UnityEngine;

namespace Game._Scripts {
  public class SoundManager : MonoBehaviour {
    public static SoundManager Instance;
    
    [SerializeField] private AudioClip hammerOnHitSound;
    [SerializeField] private AudioClip gameOverSound;

    private void Awake() {
      if (Instance) {
        Destroy(gameObject);
      }else {
        Instance = this;
      }
      DontDestroyOnLoad(gameObject);

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