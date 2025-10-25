using UnityEngine;
using Random = UnityEngine.Random;

namespace Game._Scripts {
  public class ImageManager : MonoBehaviour {
    public static ImageManager Instance { get; private set; }

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
    private Sprite[] images;


    public Sprite GetRandomImage() {
      return images[Random.Range(0, images.Length)];
    }
  }
}