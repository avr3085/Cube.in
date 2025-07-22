using System;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField] private Entity controller;
    [SerializeField, Range(1, 10)] private int waitTime = 2;
    [SerializeField] private Transform nozzle;

    [Header("Broadcasting Channel")]
    [SerializeField] private MissileRequestHandler missileRequestHandler = default;

    private float elapsedTime = 0f;
    private Vector3 lookDirection;

    private void Update()
    {
        if (elapsedTime < waitTime)
        {
            elapsedTime += Time.deltaTime;
        }
        else
        {
            int res = controller.CheckOverlapsBox();
            if (res > 1)
            {
                foreach (Collider col in controller.Colls)
                {
                    var item = col.GetComponent<Entity>();
                    if (item != controller)
                    {
                        lookDirection = item.Position - controller.Position;
                        transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
                        missileRequestHandler.Raise(MissileType.Missile, nozzle);
                        break;
                    }
                }
            }
            elapsedTime = 0f;
        }
    }
}