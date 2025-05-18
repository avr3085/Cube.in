using UnityEngine;
using System.Collections.Generic;
using Misc;

public class BotAI : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private int moveSpeed = 1;
    [SerializeField, Range(1, 10)] private int rotationSpeed = 2;
    // [SerializeField, Range(1, 100)] private int boundry = 46;
    [SerializeField, Range(1, 10)] private int visibleRange = 4; // Defines how far the bot can see

    private Rigidbody rb;
    private Vector3 rbVelocity, rotationAngle;
    private Vector2 rotVector;

    public Vector3 Pos => new Vector3(transform.position.x, 0f, transform.position.z);
    public float RotAngle => Mathf.Atan2(rotVector.x, rotVector.y) * Mathf.Rad2Deg;

    //Bot properties
    private BotIntention intention;
    private float turnFactor = 0.05f;
    private int currentHash = -1;
    private IEnumerable<int> hashArray;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        intention = BotIntention.Petrol;
        rotVector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rotationAngle = new Vector3(0f, RotAngle, 0f);
    }

    // private void Update()
    // {
    //     // Code here
    // }

    private void FixedUpdate()
    {
        rbVelocity = rb.velocity;
        rbVelocity = transform.forward * moveSpeed;
        rb.velocity = rbVelocity;

        // rb.rotation = Quaternion.Lerp(rb.rotation, Quaternion.Euler(rotationAngle), Time.fixedDeltaTime * rotationSpeed);
        var rot = Quaternion.Slerp(rb.rotation, Quaternion.Euler(rotationAngle), Time.fixedDeltaTime * rotationSpeed);
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
        Vector3 mPos = new Vector3(Pos.x - 0.5f, 0f, Pos.z - 0.5f);
        if(currentHash != mPos.ToHash())
        {
            currentHash = mPos.ToHash();
            // hashArray = Pos.ToBBoxHash();
            hashArray = Pos.VisibleRangeHash(visibleRange);
        }

        if(hashArray == null) return;

        Vector3 nearestPos = Vector3.zero;
        float maxSqrdDistaceCheck = HelperUtils.MaxSqrdDistaceCheck;
        foreach(int hashKey in hashArray)
        {
            
            Vector3 currentNearest = ResFactoryManager.Instance.GetNearest(hashKey, Pos);
            float distSqrd = (currentNearest - Pos).sqrMagnitude;
            if(distSqrd < maxSqrdDistaceCheck)
            {
                maxSqrdDistaceCheck = distSqrd;
                nearestPos = currentNearest;
            }
        }

        // Move the bot to the nearestPos
        Vector3 newRotVector = nearestPos - Pos;
        // Vector2 newDir = rotVector - new Vector2(nearestPos.x, nearestPos.z);
        rotVector -= new Vector2(newRotVector.x, newRotVector.z).normalized;

        rotationAngle = new Vector3(0f, RotAngle, 0f);

    }

    private void WrapAround()
    {
        Vector2 currentRotVector = rotVector;
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

        Vector2 newDir = rotVector - currentRotVector;
        rotVector -= newDir.normalized;

        rotationAngle = new Vector3(0f, RotAngle, 0f);
    }

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
            dHashArray = Pos.VisibleRangeV3(visibleRange);
        }

        if (dHashArray == null) return;

        foreach (var item in dHashArray)
        {
            Gizmos.DrawSphere(item, 0.1f);
        }



    }
    #endif

}