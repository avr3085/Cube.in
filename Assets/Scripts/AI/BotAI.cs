using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Misc;

public class BotAI : Entity
{
    [SerializeField, Range(1, 10)] private int moveSpeed = 1;
    [SerializeField, Range(1, 10)] private int rotationSpeed = 2;
    [SerializeField, Range(1, 10)] private int visibleRange = 4; // how far the bot can see
    [SerializeField] private bool debugMode = false;

    [Header("Enemy Collision Check")]
    [SerializeField, Range(1, 10)] int maxCollision = 5;
    [SerializeField, Range(1, 10)] int halfRadius = 1;
    [SerializeField] private LayerMask mask = default;
    [SerializeField] private float alignForce = 0.05f;

    private Rigidbody rb;
    private Vector3 velocity;
    private Vector3 rotVector;
    private Collider[] hitColliders;

    public Vector3 Pos => new Vector3(transform.position.x, 0f, transform.position.z);
    public float RotAngle => Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;

    public override Vector3 Position => Pos;
    public override Vector3 Velocity => Velocity;

    //Bot properties
    private BotIntention intention;
    private float turnFactor = 5f;
    private int currentHash = -1;
    private IEnumerable<int> hashArray;

    // Petrol
    private bool hasPatrolPoint = false;
    private Vector3 patrolPoint;

    private float combatTimer;
    private Entity combatTarget = null;

    // mEntity Ref
    private Entity mEntity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        intention = BotIntention.Patrol; // setting default intention
        hitColliders = new Collider[maxCollision];
        mask = LayerMask.GetMask("Combat");

        velocity = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        rotVector = new Vector3(0f, RotAngle, 0f);
        mEntity = GetComponent<Entity>();
    }

    private void Update()
    {
        int nearByTargets = Physics.OverlapBoxNonAlloc(transform.position, Vector3.one * halfRadius,
                hitColliders, Quaternion.identity, mask);

        // Will switch to combat only if there is a valid target around, and the bot is on patrol
        if (combatTarget == null && nearByTargets > 1)
        {
            var tList = hitColliders.Where(i => i != null).Select(o => o.GetComponent<Entity>()).ToList();
            // tList.Remove(this);
            tList.Remove(mEntity);

            foreach (var e in tList)
            {
                if (e != mEntity)
                {
                    combatTarget = e;
                    intention = BotIntention.Combat;
                    combatTimer = Random.Range(5, 20);
                    break;
                }
            }
        }

        Tick(intention);
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity * moveSpeed;
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
                Survive();
                break;
            
            case BotIntention.Combat:
                Combat();
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

    private void Combat()
    {
        // Make the bot steer
        if (combatTimer > 0f)
        {
            // The bot will steer till then
            Vector3 mRight = transform.right;
            Vector3 targetDirection = combatTarget.Position - Pos;

            float dotProd = HelperUtils.DotProduct(mRight, targetDirection);
            if (dotProd > 0f)
            {
                // steer right
                Vector3 newDir = velocity + (targetDirection + mRight);
                velocity = newDir.normalized;
                rotVector = new Vector3(0f, RotAngle, 0f);
            }
            else
            {
                // steer left
                Vector3 newDir = velocity + (targetDirection + mRight * -1);
                velocity = newDir.normalized;
                rotVector = new Vector3(0f, RotAngle, 0f);
            }
            combatTimer -= Time.deltaTime;
        }
        else
        {
            //
            combatTarget = null;
            // Debug.Log("Switch to patrol mode");
            intention = BotIntention.Patrol;
        }
        
    }
    private void Survive()
    {
        /**
         In Survive mode the bot will move away from other bots
        */
    }

    private void WrapAround()
    {
        Vector3 currentRotVector = velocity;
        if (Pos.x < -HelperUtils.BoundsOffset)
        {
            currentRotVector.x += turnFactor;
        }
        if (Pos.x > HelperUtils.BoundsOffset)
        {
            currentRotVector.x -= turnFactor;
        }
        if (Pos.z < -HelperUtils.BoundsOffset)
        {
            currentRotVector.z += turnFactor;
        }
        if (Pos.z > HelperUtils.BoundsOffset)
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