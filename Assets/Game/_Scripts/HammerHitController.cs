using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game._Scripts {
  public class HammerHitController : MonoBehaviour {
    public static event Action OnHammerHit;
    public static event Action OnRepeatingButtonsHit;

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
          OnHammerHit?.Invoke();
        }
      }
      else if (spacePressed) {
        if (_lastButtonPressed == HitButton.Space) {
          OnRepeatingButtonsHit?.Invoke();
          // Debug.Log("Repeating buttons hit: space clicked");
        }
        else {
          _lastButtonPressed = HitButton.Space;
          OnHammerHit?.Invoke();
        }
      }
    }
  }
}