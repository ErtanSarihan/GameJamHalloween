using UnityEngine;
using Random = UnityEngine.Random;

namespace Game._Scripts {
  public class ImageManager : MonoBehaviour {
    public static ImageManager Instance { get; private set; }

    private void Awake() {
      if (Instance) {
        Destroy(gameObject);
      }
      else {
        Instance = this;
      }
      DontDestroyOnLoad(gameObject);
    }

    [SerializeField] private Sprite[] _images;
    
    
    public Sprite GetRandomImage() {
      return _images[Random.Range(0, _images.Length)];
    }
  }
}