using UnityEngine;
using System.Collections.Generic;
using Misc;

public class BotAI : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private int moveSpeed = 1;
    [SerializeField, Range(1, 10)] private int rotationSpeed = 2;
    [SerializeField, Range(1, 10)] private int visibleRange = 4; // how far the bot can see

    private Rigidbody rb;
    private Vector3 rbVelocity, rotVector;
    private Vector2 rotDirection;

    public Vector3 Pos => new Vector3(transform.position.x, 0f, transform.position.z);
    public float RotAngle => Mathf.Atan2(rotDirection.x, rotDirection.y) * Mathf.Rad2Deg;

    //Bot properties
    private BotIntention intention;
    private float turnFactor = 5f;
    private int currentHash = -1;
    private IEnumerable<int> hashArray;

    // Petrol
    private bool hasPetrolDirection = false;
    private Vector3 petrolDirection;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        intention = BotIntention.Petrol;
        rotDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)); //Picking a random Direction for the bot
        rotVector = new Vector3(0f, RotAngle, 0f);
    }

    private void Update()
    {
        Tick(intention);
    }

    private void FixedUpdate()
    {
        rbVelocity = rb.velocity;
        rbVelocity = transform.forward * moveSpeed;
        rb.velocity = rbVelocity;

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
            case BotIntention.Petrol:
                Petrol();
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

    private void Petrol()
    {
        if (hasPetrolDirection)
        {
            float petrolTargetDistance = (petrolDirection - Pos).sqrMagnitude;

            if (petrolTargetDistance > visibleRange * visibleRange)
            {
                hasPetrolDirection = false;
            }
            else
            {
                return;
            }
        }

        Vector3 pPos = new Vector3(Pos.x - 0.5f, 0f, Pos.z - 0.5f);
        if (currentHash != pPos.ToHash())
        {
            currentHash = pPos.ToHash();
            hashArray = Pos.BoxVisionHash(visibleRange);
        }

        if (hashArray == null) return;

        foreach (int hashKey in hashArray)
        {
            if (ResFactoryManager.Instance.ContainsKey(hashKey))
            {
                Vector3 curNearestTarget = ResFactoryManager.Instance.GetNearestResource(hashKey, Pos);
                float distSqrd = (curNearestTarget - Pos).sqrMagnitude;
                if (distSqrd < visibleRange * visibleRange && distSqrd > 1f)
                {
                    petrolDirection = curNearestTarget;
                    hasPetrolDirection = true;
                    break;
                }
            }
        }

        //rotate the bot to the new direction
        Vector3 newPetrolDir = petrolDirection - Pos;
        Vector2 newDir = rotDirection + new Vector2(newPetrolDir.x, newPetrolDir.z);
        // Vector2 newDir = new Vector2(newPetrolDir.x, newPetrolDir.z) - rotDirection;
        rotDirection = newDir.normalized;

        rotVector = new Vector3(0f, RotAngle, 0f);

    }

    private void WrapAround()
    {
        
        Vector2 currentRotVector = rotDirection;
        if(Pos.x < -HelperUtils.MapBoundry)
        {
            currentRotVector.x += turnFactor;
        }
        if(Pos.x > HelperUtils.MapBoundry)
        {
            currentRotVector.x -= turnFactor;
        }
        if(Pos.z < -HelperUtils.MapBoundry)
        {
            currentRotVector.y += turnFactor;
        }
        if(Pos.z > HelperUtils.MapBoundry)
        {
            currentRotVector.y -= turnFactor;
        }

        Vector2 newDir = rotDirection + currentRotVector;
        rotDirection = newDir.normalized;

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
        if (!Application.isPlaying) return;

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

        if (!hasPetrolDirection) return;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(petrolDirection, 0.2f);

    }
    #endif

}