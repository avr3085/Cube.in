using System;
using UnityEngine;

//smooth camera follow script

//Todo --> fix the camera movement, make it more smooth
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField, Range(1, 10)] private int smoothing = 5;

    [Header("Listening Channel"), SerializeField] private Vector3EventListener targetPositionListener = default;

    private void OnEnable()
    {
        targetPositionListener.onEventRaised += FollowTarget;
    }

    private void OnDisable()
    {
        targetPositionListener.onEventRaised -= FollowTarget;
    }

    private void FollowTarget(Vector3 val)
    {
        var desiredPos = val + offset;
        var smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothing * Time.deltaTime);
        transform.position = smoothPos;
    }

} 