using System;
using UnityEngine;

/// <summary>
/// Enables Camera to follow the target, with a smooth movement.
/// Camera will always maintain a distance(offset) from the target.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField, Range(1, 10)] private int smoothing = 5;
    [SerializeField] private Transform target;
    
    private void LateUpdate()
    {
        FollowTarget(target.position);
    }
    
    /// <summary>
    /// Updating camera position by given input
    /// </summary>
    /// <param name="targetPosition">Current target position</param>
    private void FollowTarget(Vector3 targetPosition)
    {
        Vector3 desiredPos = targetPosition + offset;
        Vector3 smoothPos = Vector3.MoveTowards(transform.position, desiredPos, smoothing * Time.deltaTime);
        transform.position = smoothPos;
    }

} 