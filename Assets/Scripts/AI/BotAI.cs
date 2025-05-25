using UnityEngine;
using System.Collections.Generic;
using Misc;

public class BotAI : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private int moveSpeed = 1;
    [SerializeField, Range(1, 10)] private int rotationSpeed = 2;
    [SerializeField, Range(1, 10)] private int visibleRange = 4; // how far the bot can see
    [SerializeField] private bool debugMode = false;

    private Rigidbody rb;
    // private Vector3 rbVelocity, rotVector;
    // private Vector2 rotDirection;

    private Vector3 velocity;
    private Vector3 rotVector;

    public Vector3 Pos => new Vector3(transform.position.x, 0f, transform.position.z);
    // public float RotAngle => Mathf.Atan2(rotDirection.x, rotDirection.y) * Mathf.Rad2Deg;
    public float RotAngle => Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;

    //Bot properties
    private BotIntention intention;
    private float turnFactor = 5f;
    private int currentHash = -1;
    private IEnumerable<int> hashArray;

    // Petrol
    private bool hasPatrolPoint = false;
    private Vector3 patrolPoint;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        intention = BotIntention.Patrol;
        // rotDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)); //Picking a random Direction for the bot
        // rotVector = new Vector3(0f, RotAngle, 0f);

        velocity = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        rotVector = new Vector3(0f, RotAngle, 0f);

    }

    private void Update()
    {
        Tick(intention);
    }

    private void FixedUpdate()
    {
        // rbVelocity = rb.velocity;
        // rbVelocity = transform.forward * moveSpeed;
        // rb.velocity = rbVelocity;

        rb.velocity = velocity * moveSpeed;

        // rb.rotation = Quaternion.Lerp(rb.rotation, Quaternion.Euler(rotationAngle), Time.fixedDeltaTime * rotationSpeed);
        var rot = Quaternion.Slerp(rb.rotation, Quaternion.Euler(rotVector), Time.fixedDeltaTime * rotationSpeed);
        rb.MoveRotation(rot);
    }

    private void LateUpdate()
    {
        WrapAround();   
    }

    private void Tick(BotIntention currentIntention)
    {
        switch(currentIntention)
        {   
            case BotIntention.Patrol:
                Patrol();
                break;
            
            case BotIntention.Survive:
                break;
            
            case BotIntention.Combat:
                break;
            
            default:
                // Do Nothing
                break;
        }
    }

    private void Patrol()
    {
        if (hasPatrolPoint)
        {
            float patrolOffset = (patrolPoint - Pos).sqrMagnitude;
            if (patrolOffset < 1f || patrolOffset > visibleRange * visibleRange)
            {
                hasPatrolPoint = false;
            }
            return;
        }

        Vector3 pPos = new Vector3(Pos.x - 0.5f, 0f, Pos.z - 0.5f);

        if (currentHash == pPos.ToHash()) return;

        currentHash = pPos.ToHash();
        hashArray = Pos.BoxVisionHash(visibleRange);

        if (hashArray == null) return;

        Vector3 nearestTarget;
        foreach (var hashKey in hashArray)
        {
            if (ResFactoryManager.Instance.ContainsKey(hashKey))
            {
                nearestTarget = ResFactoryManager.Instance.NearestRes(hashKey, Pos);
                if (!nearestTarget.Equals(Vector3.zero))
                {
                    float distSqrd = (nearestTarget - Pos).sqrMagnitude;
                    if (distSqrd < visibleRange * visibleRange && distSqrd > 1f)
                    {
                        patrolPoint = nearestTarget;
                        hasPatrolPoint = true;

                        Vector3 newDir = velocity + (patrolPoint - Pos);
                        velocity = newDir.normalized;
                        rotVector = new Vector3(0f, RotAngle, 0f);
                        break;
                    }
                }
            }
        }
    }

    private void WrapAround()
    {
        Vector3 currentRotVector = velocity;
        if (Pos.x < -HelperUtils.BoundsOffset)
        {
            currentRotVector.x += turnFactor;
        }
        if(Pos.x > HelperUtils.BoundsOffset)
        {
            currentRotVector.x -= turnFactor;
        }
        if(Pos.z < -HelperUtils.BoundsOffset)
        {
            currentRotVector.z += turnFactor;
        }
        if(Pos.z > HelperUtils.BoundsOffset)
        {
            currentRotVector.z -= turnFactor;
        }

        Vector3 newDir = velocity + currentRotVector;
        velocity = newDir.normalized;

        rotVector = new Vector3(0f, RotAngle, 0f);
    }

/// <summary>
/// Debug Area, Should avoid adding to the final code
/// </summary>
#if UNITY_EDITOR

    private int dCurrentHash = -1;
    private IEnumerable<Vector3> dHashArray;
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying && debugMode) return;

        Gizmos.color = Color.green;
        Vector3 mPos = new Vector3(Pos.x - 0.5f, 0f, Pos.z - 0.5f);
        if (dCurrentHash != mPos.ToHash())
        {
            dCurrentHash = mPos.ToHash();
            // hashArray = Pos.ToBBoxHash();
            dHashArray = Pos.BoxVisionV3(visibleRange);
        }

        if (dHashArray == null) return;

        foreach (var item in dHashArray)
        {
            Gizmos.DrawSphere(item, 0.1f);
        }

        if (!hasPatrolPoint) return;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(patrolPoint, 0.2f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(Pos, patrolPoint);
        Gizmos.DrawLine(Pos, Pos + velocity);

    }
    #endif

}