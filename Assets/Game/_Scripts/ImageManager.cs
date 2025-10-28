using UnityEngine;
using Random = UnityEngine.Random;

namespace Game._Scripts {
  public class ImageManager : MonoBehaviour {
    public static ImageManager Instance { get; private set; }
    
    private static int _lastReturnedIndex = -1;
    private static int _index;

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
      do {
        _index = Random.Range(0, images.Length);
      } while (_index == _lastReturnedIndex);
      
      _lastReturnedIndex = _index;
      return images[_index];
    }
  }
}