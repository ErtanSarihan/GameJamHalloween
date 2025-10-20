using System.Collections;
using UnityEngine;

namespace Game._Scripts {
  public class RubberHammerAnimator : MonoBehaviour {
    private Transform _rubberHammerTransform;
    [SerializeField] private float animationDuration = 0.3f;

    private void Start() {
      _rubberHammerTransform = transform;
      HammerHitController.OnHammerHit += OnHammerHit;
    }

    private void OnHammerHit() {
      StartCoroutine(HammerHitAnimation());
    }

    private IEnumerator HammerHitAnimation() {
      float elapsed = 0f;
      float halfDuration = animationDuration / 2f;

      // Rotate from 0 to -90
      while (elapsed < halfDuration) {
        elapsed += Time.deltaTime;
        float t = elapsed / halfDuration;
        float angle = Mathf.Lerp(0f, -90f, t);
        _rubberHammerTransform.localRotation = Quaternion.Euler(angle, 0f, 90f);
        yield return null;
      }

      elapsed = 0f;

      // Rotate from -90 back to 0
      while (elapsed < halfDuration) {
        elapsed += Time.deltaTime;
        float t = elapsed / halfDuration;
        float angle = Mathf.Lerp(-90f, 0f, t);
        _rubberHammerTransform.localRotation = Quaternion.Euler(angle, 0f, 90f);
        yield return null;
      }

      // Ensure final rotation is exactly 0
      _rubberHammerTransform.localRotation = Quaternion.Euler(0f, 0f, 90f);
    }
  }
}