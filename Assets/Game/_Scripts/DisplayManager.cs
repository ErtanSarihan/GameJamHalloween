using UnityEngine;
using UnityEngine.UI;

namespace Game._Scripts {
  public class DisplayManager : MonoBehaviour {
    public static DisplayManager Instance;

    private void Awake() {
      if (Instance) {
        Destroy(gameObject);
      }
      else {
        Instance = this;
      }
      DontDestroyOnLoad(gameObject);
    }


    [SerializeField]
    private Image imageDisplay;
    [SerializeField]
    private Image glitchImage;
    public bool isGlitching;

    [SerializeField]
    [Range(0f, 1f)]
    [Tooltip("Probability of showing glitch when setting a random image (0 = never, 1 = always)")]
    private float glitchChance = 0.3f;

    private void Start() {
      SetRandomImage();
      HideGlitch();
    }

    public void SetRandomImage() {
      Sprite image = ImageManager.Instance.GetRandomImage();
      imageDisplay.sprite = image;

      // Roll the dice to determine if we should show or hide the glitch
      float roll = Random.Range(0f, 1f);
      if (roll <= glitchChance) {
        ShowGlitch();
      }
      else {
        HideGlitch();
      }
    }

    public void ShowGlitch() {
      isGlitching = true;
      glitchImage.gameObject.SetActive(true);
    }

    public void HideGlitch() {
      isGlitching = false;
      glitchImage.gameObject.SetActive(false);
    }
  }
}