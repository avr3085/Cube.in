using System;
using UnityEngine;

//smooth camera follow script

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField, Range(1, 10)] private int smoothing = 5;
    [SerializeField] private Transform target;
    

    private void LateUpdate()
    {
        FollowTarget(target.position);
    }
    private void FollowTarget(Vector3 targetPosition)
    {
        Vector3 desiredPos = targetPosition + offset;
        Vector3 smoothPos = Vector3.MoveTowards(transform.position, desiredPos, smoothing * Time.deltaTime);
        transform.position = smoothPos;
    }

} 