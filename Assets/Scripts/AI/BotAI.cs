using UnityEngine;
using Misc;

public class BotAI : MonoBehaviour
{
    [Header("Bot Properties")]
    [SerializeField, Range(1,10)] private int moveSpeed = 1;
    [SerializeField, Range(1,100)] private int rotationSpeed = 50;
    [SerializeField, Range(1, 100)] private float boundry = 48f;

    [Header("Intentions")]
    [SerializeField] private Intention intention = Intention.Roaming;

    [Tooltip("Toggle's AABB for uniform grid collision check.")]
    [SerializeField] private bool debug = false;
    private float turn = 0.1f;

    private Rigidbody rb;
    private Vector3 rbVelocity, rotationAngle;
    private Vector2 rotVector;


    public Vector2 Position => new Vector2(transform.position.x, transform.position.z);
    public float RotAngle => Mathf.Atan2(rotVector.x, rotVector.y) * Mathf.Rad2Deg;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rotVector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rotationAngle = new Vector3(0f, RotAngle, 0f);
    }

    private void Update()
    {
        if(debug)
        {
            Vector2 cPos = new Vector2(transform.position.x, transform.position.z);
            DrawDebugCube(cPos);
        }

        FreeRoamState();
        WrapAround();
    }

    private void FixedUpdate()
    {
        rbVelocity = rb.velocity;
        rbVelocity = transform.forward * moveSpeed;
        rb.velocity = rbVelocity;

        Debug.DrawLine(transform.position, transform.position + transform.forward, Color.red);

        rb.rotation = Quaternion.Lerp(rb.rotation, Quaternion.Euler(rotationAngle), Time.fixedDeltaTime * rotationSpeed);
    }

    private void FreeRoamState()
    {
        var hashArray = transform.position.ToBBoxHash();

        Vector3 avgRoamPosition = Vector3.zero;
        foreach(var hashKey in hashArray)
        {
            avgRoamPosition += ResFactoryManager.Instance.GetAveragePosition(hashKey);
        }

//[Note - add the player position here]
//Try avgRoamPosition/total array
// and try avgRoamPosition
        Vector3 roamDirection = (avgRoamPosition - transform.position).normalized;

        Vector2 newDir = rotVector - new Vector2(roamDirection.x, roamDirection.z);
        rotVector -= newDir.normalized;

        rotationAngle = new Vector3(0f, RotAngle, 0f);
    }

    private void WrapAround()
    {
        Vector2 currentVelocity = rotVector;
        if(Position.x < -boundry)
        {
            currentVelocity.x += turn;
        }
        if(Position.x > boundry)
        {
            currentVelocity.x -= turn;
        }
        if(Position.y < -boundry)
        {
            currentVelocity.y += turn;
        }
        if(Position.y > boundry)
        {
            currentVelocity.y -= turn;
        }

        Vector2 newDir = rotVector - currentVelocity;
        rotVector -= newDir.normalized;

        rotationAngle = new Vector3(0f, RotAngle, 0f);
    }


    private void DrawDebugCube(Vector2 pos)
    {
        //this code will draw a debug cube around the player
        Debug.DrawLine(new Vector3(pos.x + 0.5f, 0f, pos.y + 0.5f), new Vector3(pos.x - 0.5f, 0f, pos.y + 0.5f), Color.red);
        Debug.DrawLine(new Vector3(pos.x - 0.5f, 0f, pos.y + 0.5f), new Vector3(pos.x - 0.5f, 0f, pos.y - 0.5f), Color.red);
        Debug.DrawLine(new Vector3(pos.x - 0.5f, 0f, pos.y - 0.5f), new Vector3(pos.x + 0.5f, 0f, pos.y - 0.5f), Color.red);
        Debug.DrawLine(new Vector3(pos.x + 0.5f, 0f, pos.y - 0.5f), new Vector3(pos.x + 0.5f, 0f, pos.y + 0.5f), Color.red);

    }

}