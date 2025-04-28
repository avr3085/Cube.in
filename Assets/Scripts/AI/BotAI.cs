using UnityEngine;
using System.Collections.Generic;
using Misc;

public class BotAI : MonoBehaviour, IResCollector
{
    // [SerializeField, Range(1, 10)] private int moveSpeed = 6;
    // [SerializeField, Range(1, 100)] private int rotationSpeed = 5;
    // [SerializeField, Range(1, 100)] private int boundry = 46;

    // [Tooltip("Toggle's Debug AABB for uniform grid collision check.")]
    // [SerializeField] private bool debug = false;
    // private float turn = 0.5f;

    // private Rigidbody rb;
    // private Vector3 rotationDirection;
    
    // private Vector3 rbVelocity, rotationAngle;
    // private Vector2 rotVector;

    // public Vector3 Pos => new Vector3(transform.position.x, 0f, transform.position.z);
    // public float RotAngle => Mathf.Atan2(rotVector.x, rotVector.y) * Mathf.Rad2Deg;

    // private IEnumerable<int> hashArray;
    // private int currentHash = -1;
    // private BotIntention intention;

    private void Start()
    {
        // rb = GetComponent<Rigidbody>();
        // intention = BotIntention.Roaming;
        // rotVector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        // rotationAngle = new Vector3(0f, RotAngle, 0f);
    }

    private void Update()
    {
        // if(debug)
        // {
        //     DrawDebugCube(Pos);
        // }

        // Tick(intention);
        // WrapAround();
    }

    private void FixedUpdate()
    {
        // rbVelocity = rb.velocity;
        // rbVelocity = transform.forward * moveSpeed;
        // rb.velocity = rbVelocity;

        // Debug.DrawLine(transform.position, transform.position + transform.forward, Color.red);

        // // rb.rotation = Quaternion.Lerp(rb.rotation, Quaternion.Euler(rotationAngle), Time.fixedDeltaTime * rotationSpeed);
        // var rot = Quaternion.Slerp(rb.rotation, Quaternion.Euler(rotationAngle), Time.fixedDeltaTime * rotationSpeed);
        // rb.MoveRotation(rot);
    }

    // private void LateUpdate()
    // {
    //     WrapAround();
    // }

    public void OnResCollected(ResType resType)
    {
        // throw new System.NotImplementedException();
    }

    // private void Tick(BotIntention mIntention)
    // {
    //     switch(mIntention)
    //     {
    //         case BotIntention.Roaming:
    //             Roaming();
    //             break;

    //         case BotIntention.Survival:
    //             // Survival 
    //             break;

    //         case BotIntention.Combat:
    //             // Combat
    //             break;

    //         default:
    //             Debug.LogError("UnDefined bot intention detected");
    //             break;
    //     }
    // }

    // private void Roaming()
    // {
    //     Vector3 mPos = new Vector3(Pos.x - 0.5f, 0f, Pos.z - 0.5f);
    //     if(currentHash != mPos.ToHash())
    //     {
    //         currentHash = mPos.ToHash();
    //         hashArray = Pos.ToBBoxHash();

    //         Vector3 avgPos = Vector3.zero;
    //         int count = 0;
    //         foreach(int key in hashArray)
    //         {
    //             avgPos += ResFactoryManager.Instance.GetAveragePosition(key);
    //             count++;
    //         }

    //         if(avgPos.Equals(Vector3.zero)) return;

    //         avgPos /= count;
    //         Vector3 newPos = avgPos - Pos;
    //         rotVector += new Vector2(newPos.x, newPos.z).normalized;
    //         rotationAngle = new Vector3(0f, RotAngle, 0f);
    //     }
    // }

    // private void DrawDebugCube(Vector3 pos)
    // {
    //     //this code will draw a debug cube around the player
    //     Debug.DrawLine(new Vector3(pos.x + 0.5f, 0f, pos.z + 0.5f), new Vector3(pos.x - 0.5f, 0f, pos.z + 0.5f), Color.red);
    //     Debug.DrawLine(new Vector3(pos.x - 0.5f, 0f, pos.z + 0.5f), new Vector3(pos.x - 0.5f, 0f, pos.z - 0.5f), Color.red);
    //     Debug.DrawLine(new Vector3(pos.x - 0.5f, 0f, pos.z - 0.5f), new Vector3(pos.x + 0.5f, 0f, pos.z - 0.5f), Color.red);
    //     Debug.DrawLine(new Vector3(pos.x + 0.5f, 0f, pos.z - 0.5f), new Vector3(pos.x + 0.5f, 0f, pos.z + 0.5f), Color.red);
    // }

    // private void WrapAround()
    // {
    //     Vector2 currentVelocity = rotVector;
    //     if(Pos.x < -boundry)
    //     {
    //         currentVelocity.x += turn;
    //     }
    //     if(Pos.x > boundry)
    //     {
    //         currentVelocity.x -= turn;
    //     }
    //     if(Pos.z < -boundry)
    //     {
    //         currentVelocity.y += turn;
    //     }
    //     if(Pos.z > boundry)
    //     {
    //         currentVelocity.y -= turn;
    //     }

    //     Vector2 newDir = rotVector - currentVelocity;
    //     rotVector -= newDir.normalized;

    //     rotationAngle = new Vector3(0f, RotAngle, 0f);
    // }
}