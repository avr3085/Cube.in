using UnityEngine;
using System.Collections.Generic;
using Misc;

public class BotAI : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private int moveSpeed = 1;
    [SerializeField, Range(1, 10)] private int rotationSpeed = 2;
    [SerializeField, Range(1, 100)] private int boundry = 46;

    private Rigidbody rb;
    private Vector3 rbVelocity, rotationAngle;
    private Vector2 rotVector;

    public Vector3 Pos => new Vector3(transform.position.x, 0f, transform.position.z);
    public float RotAngle => Mathf.Atan2(rotVector.x, rotVector.y) * Mathf.Rad2Deg;

    //Bot properties
    private BotIntention intention;
    private float turn = 0.5f;
    private int currentHash = -1;
    private IEnumerable<int> hashArray;
    private IEnumerable<Vector3> hashArrayV3;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        intention = BotIntention.Roaming;
        rotVector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rotationAngle = new Vector3(0f, RotAngle, 0f);
    }

    // private void Update()
    // {
    //     // Each frame bot will decide its next action
    //     Tick(intention);
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
            case BotIntention.None:
                break;
            
            case BotIntention.Roaming:
                Roam();
                break;
            
            case BotIntention.Survival:
                break;
            
            case BotIntention.Combat:
                break;
            
            default:
                // Do Nothing
                break;
        }
    }

    private void Roam()
    {
        Vector3 mPos = new Vector3(Pos.x - 0.5f, 0f, Pos.z - 0.5f);
        if(currentHash != mPos.ToHash())
        {
            currentHash = mPos.ToHash();
            hashArray = Pos.ToBBoxHash();// Use strategy pattern, if possible -- use a bigger Range
            // hashArray = Position.ToMagBBoxHash(); // using Magnet method
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
        if(Pos.x < -boundry)
        {
            currentRotVector.x += turn;
        }
        if(Pos.x > boundry)
        {
            currentRotVector.x -= turn;
        }
        if(Pos.z < -boundry)
        {
            currentRotVector.y += turn;
        }
        if(Pos.z > boundry)
        {
            currentRotVector.y -= turn;
        }

        Vector2 newDir = rotVector - currentRotVector;
        rotVector -= newDir.normalized;

        rotationAngle = new Vector3(0f, RotAngle, 0f);
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // if(!Application.isPlaying) return;

        // Gizmos.color = Color.red;
        // if(currentHash != Pos.ToHash())
        // {
        //     currentHash = Pos.ToHash();
        //     hashArrayV3 = Pos.ToBBoxHashV3();// Use strategy pattern, if possible -- use a bigger Range
        //     // hashArray = Position.ToMagBBoxHash(); // using Magnet method
        // }

        // foreach(var item in hashArrayV3)
        // {
        //     Gizmos.DrawSphere(item, 0.1f);
        // }
        
    }
    #endif

}