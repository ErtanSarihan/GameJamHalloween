using System;
using System.Collections;
using UnityEngine;

namespace Game._Scripts {
  public class RubberHammerAnimator : MonoBehaviour {
    private Transform _rubberHammerTransform;
    [SerializeField] private float animationDuration = 0.3f;
    [SerializeField] private float initialRotationX = -110f;
    [SerializeField] private float rotationY = 180f;
    [SerializeField] private float rotationZ = 90f;
    [SerializeField] private float hitRotationX = -90f;

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

      // Rotate from initial to hit position
      while (elapsed < halfDuration) {
        elapsed += Time.deltaTime;
        float t = elapsed / halfDuration;
        float angle = Mathf.Lerp(initialRotationX, hitRotationX, t);
        _rubberHammerTransform.localRotation = Quaternion.Euler(angle, rotationY, rotationZ);
        yield return null;
      }

      elapsed = 0f;

      // Rotate from hit position back to initial
      while (elapsed < halfDuration) {
        elapsed += Time.deltaTime;
        float t = elapsed / halfDuration;
        float angle = Mathf.Lerp(hitRotationX, initialRotationX, t);
        _rubberHammerTransform.localRotation = Quaternion.Euler(angle, rotationY, rotationZ);
        yield return null;
      }

      // Ensure final rotation is exactly initial
      _rubberHammerTransform.localRotation = Quaternion.Euler(initialRotationX, rotationY, rotationZ);
    }

    private void OnDestroy() {
      HammerHitController.OnHammerHit -= OnHammerHit;
    }
  }
}