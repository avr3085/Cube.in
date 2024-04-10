using System;
using UnityEngine;

//smooth camera follow script

//Todo --> fix the camera movement, make it more smooth
public class CameraFollow : MonoBehaviour
{
    // [SerializeField] private Transform target;
    [SerializeField] private Vector2 offset;

    [Header("Listening Channel"), SerializeField] private Vector2EventListener targetPositionListener = default;

    private void OnEnable()
    {
        targetPositionListener.onEventRaised += FollowTarget;
    }

    private void OnDisable()
    {
        targetPositionListener.onEventRaised -= FollowTarget;
    }

    private void FollowTarget(Vector2 val)
    {
        transform.position = new Vector3(val.x + offset.x, val.y + offset.y, -5);
    }

} 