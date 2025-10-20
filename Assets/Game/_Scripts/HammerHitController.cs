using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game._Scripts {
  public class HammerHitController : MonoBehaviour {

    public static event Action OnHammerHit;

    private void Update() {
      if (Mouse.current.leftButton.wasPressedThisFrame || Keyboard.current.spaceKey.wasPressedThisFrame) {
        OnHammerHit?.Invoke();
      }
    }
  }
}