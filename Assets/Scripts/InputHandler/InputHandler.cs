using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Etouch = UnityEngine.InputSystem.EnhancedTouch;

/// <summary>
/// Input Handler
/// Handle all input related function here
/// </summary>

public class InputHandler : MonoBehaviour
{
    [SerializeField] private FloatingJoystick joystick;

    [Header("Broadcasting Channel")]
    [SerializeField] private Vector2EventListener inputAxisListener = default;
    [SerializeField, Range(0.1f, 0.9f)] private float validTouchPercentile = 0.85f;
    [SerializeField, Range(1, 100f)] private int minTolranceRange = 50;

    private Finger centerFinger;
    private readonly float maxKnobMovement = 100f;

    //Using new Input system for the touch input
    //handles touch/mouse input from joystick

    private void OnEnable()
    {
        Etouch.EnhancedTouchSupport.Enable();
        Etouch.Touch.onFingerDown += HandleFingerDown;
        Etouch.Touch.onFingerMove += HandleFingerMove;
        Etouch.Touch.onFingerUp += HandleFingerUp;
    }

    private void OnDisable()
    {
        Etouch.Touch.onFingerDown -= HandleFingerDown;
        Etouch.Touch.onFingerMove -= HandleFingerMove;
        Etouch.Touch.onFingerUp -= HandleFingerUp;
        Etouch.EnhancedTouchSupport.Disable();
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




    //     private void Update(){
    // #if UNITY_EDITOR

    //         //Taking Input from arrow key
    //         if(Input.GetMouseButtonDown(0))
    //         {
    //             mouseCenter = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //         }
    //         else if(Input.GetMouseButton(0))
    //         {
    //             Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    //             // mouseDrag = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //             // mouseDirection = mouseCenter - mouseDrag;
    //             // mouseDirection = Vector2.ClampMagnitude(mouseDirection, 1f);
    //             // Debug.Log(mouseDirection);

    //             // if(mouseDirection.magnitude >= 0.2f)
    //             // {
    //             //     inputAxisListener.Raise(mouseDirection);
    //             // }
    //         }

    // #endif

    // #if UNITY_WEBGL
    //         // int currentFinger = -1;
    //         // foreach(Touch t in Input.touches){
    //         //     if(t.phase != TouchPhase.Ended && t.phase != TouchPhase.Canceled){
    //         //         currentFinger++;
    //         //     }else{
    //         //         return;
    //         //     }

    //         //     if(currentFinger == 0){
    //         //         if(t.phase == TouchPhase.Began){
    //         //             mousePositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //         //             Vector2 finalPos = new Vector2(mousePositon.x + 4.5f, mousePositon.y + 4.5f);
    //         //             if(finalPos.x>=0 && finalPos.x<=9 && finalPos.y>=0 && finalPos.y<=9) mousePositionListener.Raise(finalPos);
    //         //         }
    //         //     }else{
    //         //         return;
    //         //     }
    //         // }
    // #endif

    // TODO : make sure to add input function for andorid platform

}


// #region DebugRegion
//     private void OnDrawGizmos(){
//          for Debug Purpose
//     }
// #endregion

// }