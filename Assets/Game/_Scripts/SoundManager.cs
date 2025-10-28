using UnityEngine;

namespace Game._Scripts {
  public class SoundManager : MonoBehaviour {
    public static SoundManager Instance;

    [Header("Audio Clips")]
    [SerializeField]
    private AudioClip hammerOnHitSound;
    [SerializeField]
    private AudioClip gameOverSound;
    [SerializeField]
    private AudioClip backgroundMusic;

    [Header("Volume Controls")]
    [SerializeField]
    [Range(0f, 1f)]
    private float musicVolume = 0.7f;
    [SerializeField]
    [Range(0f, 1f)]
    private float sfxVolume = 1f;

    private AudioSource backgroundMusicSource;
    private AudioSource sfxSource;

    private void Awake() {
      if (Instance == null) {
        Instance = this;
        DontDestroyOnLoad(gameObject);
      }
      else {
        Destroy(gameObject);
      }

      // Create AudioSource for background music
      backgroundMusicSource = gameObject.AddComponent<AudioSource>();
      backgroundMusicSource.loop = true;
      backgroundMusicSource.volume = musicVolume;

      // Create AudioSource for sound effects
      sfxSource = gameObject.AddComponent<AudioSource>();
      sfxSource.volume = sfxVolume;
    }

    private void OnEnable() {
      HammerHitController.OnGlitchingImageHit += OnHammerHit;
      HammerHitController.OnNonGlitchingImageHit += OnHammerHit;
      GameManager.OnGameStarted += OnGameStarted;
      GameManager.OnRestartGame += OnGameRestarted;
      GameManager.OnGameOver += OnGameOver;
    }


    private void OnGameOver() {
      backgroundMusicSource.Stop();
      sfxSource.PlayOneShot(gameOverSound);
    }

    private void OnGameStarted() {
      backgroundMusicSource.clip = backgroundMusic;
      backgroundMusicSource.Play();
    }
    
    private void OnGameRestarted() {
      sfxSource.Stop();
      backgroundMusicSource.Play();
    }


    private void OnHammerHit() {
      sfxSource.PlayOneShot(hammerOnHitSound);
    }

    private void OnDisable() {
      HammerHitController.OnGlitchingImageHit -= OnHammerHit;
      HammerHitController.OnNonGlitchingImageHit -= OnHammerHit;
      GameManager.OnGameStarted -= OnGameStarted;
      GameManager.OnRestartGame -= OnGameRestarted;
      GameManager.OnGameOver -= OnGameOver;
    }
  }
}