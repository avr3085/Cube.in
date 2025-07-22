using UnityEngine;
using Misc;

/// <summary>
/// Bot Controller script
/// Manages all the AI related behaviour of the bot
/// The script is responible for movement of the bot in the scene
/// </summary>
[RequireComponent(typeof(BotCustomization))]
public class BotAIController : Entity
{
    [SerializeField] private BotStats botStats = default;
    [HideInInspector] public Vector3 rotVector;

    // Patrol state variables
    [HideInInspector] public bool hasPatrolPoint;
    [HideInInspector] public Vector3 patrolPoint;

    // Comabt state variables
    [HideInInspector] public bool hasCombatTarget;
    [HideInInspector] public Entity comabtTarget;

    [Header("FSM")]
    [SerializeField] private State currentState = default;
    
    private const int MAX_COLLS = 5;
    private Collider[] colls;
    private Rigidbody rb;
    private Vector3 velocity;

    public override Vector3 Position => new Vector3(transform.position.x, 0f, transform.position.z);
    public override Vector3 Velocity => velocity;
    public float RotAngle => Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
    public BotStats Stats => botStats;
    public override Collider[] Colls => colls;
    public override Rigidbody RBody => rb;

    public Vector3 SetVelocity
    {
        set => velocity = value;
    }

    public Vector3 SetPosition
    {
        set
        {
            transform.position = value;
        }
    }

    public Vector3 UpdateRotVector() => rotVector = new Vector3(0f, RotAngle, 0f);

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocity = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        rotVector = new Vector3(0f, RotAngle, 0f);
        colls = new Collider[MAX_COLLS];

        currentState.Init();
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity * botStats.moveSpeed;
        var rot = Quaternion.Lerp(rb.rotation, Quaternion.Euler(rotVector), Time.fixedDeltaTime * botStats.rotationSpeed);
        rb.MoveRotation(rot);
    }

    private void LateUpdate()
    {
        WrapAround();
    }

    private void WrapAround()
    {
        Vector3 currentRotVector = velocity;
        if (Position.x < -HelperUtils.BoundsOffset)
        {
            currentRotVector.x += botStats.turnFactor;
        }
        if (Position.x > HelperUtils.BoundsOffset)
        {
            currentRotVector.x -= botStats.turnFactor;
        }
        if (Position.z < -HelperUtils.BoundsOffset)
        {
            currentRotVector.z += botStats.turnFactor;
        }
        if (Position.z > HelperUtils.BoundsOffset)
        {
            currentRotVector.z -= botStats.turnFactor;
        }

        Vector3 newDir = velocity + currentRotVector;
        velocity = newDir.normalized;

        rotVector = new Vector3(0f, RotAngle, 0f);
    }

    public void SwitchState(State nextState)
    {
        if (nextState != currentState)
        {
            currentState = nextState;
            currentState.Init();
        }
    }

    public override int CheckOverlapsBox()
    {
        return Physics.OverlapBoxNonAlloc(Position, Vector3.one * botStats.halfRadius, colls, Quaternion.identity, botStats.lMask);
    }

    public override void TakeDamage(int amount)
    {
        
    }
}