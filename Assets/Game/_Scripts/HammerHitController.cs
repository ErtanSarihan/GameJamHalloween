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
    private bool _isGameOver;
    private bool _isGameStart;

    private void OnEnable() {
      GameManager.OnGameStarted += OnGameStarted;
      GameManager.OnGameOver += OnGameOver;
      GameManager.OnRestartGame += OnRestartGame;
    }


    private void Update() {
      if (!_isGameStart) return;
      bool mousePressed = Mouse.current.leftButton.wasPressedThisFrame;
      bool spacePressed = Keyboard.current.spaceKey.wasPressedThisFrame;

      if (mousePressed && !_isGameOver) {
        if (_lastButtonPressed == HitButton.Mouse) {
          OnRepeatingButtonsHit?.Invoke();
          // Debug.Log("Repeating buttons hit: mouse left clicked");
        }
        else {
          _lastButtonPressed = HitButton.Mouse;
          HammerHit();
        }
      }
      else if (spacePressed && !_isGameOver) {
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
        OnGlitchingImageHit?.Invoke();
        DisplayManager.Instance.HideGlitch();
      }
      else {
        OnNonGlitchingImageHit?.Invoke();
      }
    }

    private void OnGameOver()
    {
      _isGameOver = true;
    }
    
    private void OnGameStarted() {
      _isGameStart = true;
    }


    private void OnRestartGame() {
      _lastButtonPressed = HitButton.None;
      _isGameOver = false;
    }


    private void OnDisable() {
      GameManager.OnGameStarted -= OnGameStarted;
      GameManager.OnGameOver -= OnGameOver;
      GameManager.OnRestartGame -= OnRestartGame;
    }
  }
}