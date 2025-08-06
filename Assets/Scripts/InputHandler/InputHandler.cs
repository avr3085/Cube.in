using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Etouch = UnityEngine.InputSystem.EnhancedTouch;

/// <summary>
/// Input Handler
/// Handling input from dynamic touch joystick and mouse
/// [Using new input system]
/// [handles mouse/touch input's, in different touch state. ex - OnFingerDown, OnFingerMoved, etc.]
/// </summary>

public class InputHandler : MonoBehaviour
{
    [SerializeField] private FloatingJoystick joystick;

    [Header("Broadcasting Channel")]
    [SerializeField] private Vector2EventListener inputAxisListener = default;
    [SerializeField, Range(0.1f, 0.9f)] private float validTouchPercentile = 0.85f;
    [SerializeField, Range(1, 100f)] private int minTolranceRange = 50;

    [Header("Listening Channel"), SerializeField] private VoidEventListener gameOverRequestHandler = default;

    private Finger centerFinger;
    private readonly float maxKnobMovement = 100f;

    private void OnEnable()
    {
        Etouch.EnhancedTouchSupport.Enable();
        Etouch.Touch.onFingerDown += HandleFingerDown;
        Etouch.Touch.onFingerMove += HandleFingerMove;
        Etouch.Touch.onFingerUp += HandleFingerUp;
        gameOverRequestHandler.onEventRaised += DisableJoystick;
    }

    private void OnDisable()
    {
        Etouch.Touch.onFingerDown -= HandleFingerDown;
        Etouch.Touch.onFingerMove -= HandleFingerMove;
        Etouch.Touch.onFingerUp -= HandleFingerUp;
        Etouch.EnhancedTouchSupport.Disable();
        gameOverRequestHandler.onEventRaised -= DisableJoystick;
    }

    /// <summary>
    /// This Function will disable the joystick movement when player is dead
    /// </summary>
    private void DisableJoystick()
    {
        joystick.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void HandleFingerDown(Finger finger)
    {
        float screenBoundary = Screen.height * validTouchPercentile;
        if(centerFinger == null && finger.screenPosition.y < screenBoundary)
        {
            centerFinger = finger;
            joystick.gameObject.SetActive(true);
            joystick.JoystickRect.anchoredPosition = finger.screenPosition;
        }
    }

    private void HandleFingerMove(Finger finger)
    {
        if(finger == centerFinger)
        {
            Vector2 knobPosition;
            Vector2 moveDirection = finger.screenPosition - joystick.JoystickRect.anchoredPosition;
            float moveDirectionSqrMagnitude = moveDirection.sqrMagnitude;
            if(moveDirectionSqrMagnitude > maxKnobMovement * maxKnobMovement)
            {
                knobPosition = moveDirection.normalized * maxKnobMovement;
            }
            else
            {
                knobPosition = moveDirection;
            }
            
            joystick.Knob.anchoredPosition = knobPosition;
            if(moveDirectionSqrMagnitude > minTolranceRange * minTolranceRange)
            {
                inputAxisListener.Raise(moveDirection.normalized);
            }
        }
    }

    private void HandleFingerUp(Finger finger)
    {
        centerFinger = null;
        joystick.Knob.anchoredPosition = Vector2.zero;
        joystick.gameObject.SetActive(false);
    }
}