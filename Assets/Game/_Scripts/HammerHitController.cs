using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game._Scripts {
  public class HammerHitController : MonoBehaviour {
    public static event Action OnRepeatingButtonsHit;
    public static event Action OnNonGlitchingImageHit;
    public static event Action OnGlitchingImageHit;

    private enum HitButton {
      None,
      Mouse,
      Space
    }

    private HitButton _lastButtonPressed = HitButton.None;

    private void Update() {
      bool mousePressed = Mouse.current.leftButton.wasPressedThisFrame;
      bool spacePressed = Keyboard.current.spaceKey.wasPressedThisFrame;

      if (mousePressed) {
        if (_lastButtonPressed == HitButton.Mouse) {
          OnRepeatingButtonsHit?.Invoke();
          // Debug.Log("Repeating buttons hit: mouse left clicked");
        }
        else {
          _lastButtonPressed = HitButton.Mouse;
          HammerHit();
        }
      }
      else if (spacePressed) {
        if (_lastButtonPressed == HitButton.Space) {
          OnRepeatingButtonsHit?.Invoke();
          // Debug.Log("Repeating buttons hit: space clicked");
        }
        else {
          _lastButtonPressed = HitButton.Space;
          HammerHit();
        }
      }
    }


    private void HammerHit() {
      if (DisplayManager.Instance.isGlitching) {
        // score ++ 
        OnGlitchingImageHit?.Invoke();
        DisplayManager.Instance.HideGlitch();
      }
      else {
        OnNonGlitchingImageHit?.Invoke();
      }
    }
  }
}