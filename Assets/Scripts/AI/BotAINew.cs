using UnityEngine;
using System.Collections.Generic;
using Misc;

public class BotAINew : MonoBehaviour
{
    // [SerializeField, Range(1, 10)] private int moveSpeed = 1;
    // [SerializeField, Range(1, 10)] private int rotationSpeed = 2;
    // [SerializeField, Range(1, 10)] private int visibleRange = 4; // how far the bot can see

    // private Vector3 moveDirection;
    // private Rigidbody rb;


    // private void Start()
    // {
    //     rb = GetComponent<Rigidbody>();
    //     moveDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
    // }

    // // private void Update()
    // // {

    // // }

    // private void FixedUpdate()
    // {
    //     rb.MovePosition(moveDirection * moveSpeed * Time.fixedDeltaTime);

    //     var rot = Quaternion.Slerp(rb.rotation, Quaternion.Euler(rotVector), Time.fixedDeltaTime * rotationSpeed);
    //     rb.MoveRotation(rot);
    // }

    // private void LateUpdate()
    // {
    //     WrapAround();
    // }

    // private void WrapAround()
    // {

    //     Vector2 currentRotVector = rotDirection;
    //     if (Pos.x < -HelperUtils.BoundsOffset)
    //     {
    //         currentRotVector.x += turnFactor;
    //     }
    //     if (Pos.x > HelperUtils.BoundsOffset)
    //     {
    //         currentRotVector.x -= turnFactor;
    //     }
    //     if (Pos.z < -HelperUtils.BoundsOffset)
    //     {
    //         currentRotVector.y += turnFactor;
    //     }
    //     if (Pos.z > HelperUtils.BoundsOffset)
    //     {
    //         currentRotVector.y -= turnFactor;
    //     }

    //     Vector2 newDir = rotDirection + currentRotVector;
    //     rotDirection = newDir.normalized;

    //     rotVector = new Vector3(0f, RotAngle, 0f);
    // }
}