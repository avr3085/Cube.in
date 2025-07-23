using UnityEngine;
using Misc;

/// <summary>
/// Bot Controller script
/// Manages all the AI related behaviour of the bot
/// The script is responible for movement of the bot in the scene
/// </summary>
[RequireComponent(typeof(BotCustomization))]
public class BotAIController : EntityData
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

    public float RotAngle => Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
    public BotStats Stats => botStats;

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

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        velocity = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        rotVector = new Vector3(0f, RotAngle, 0f);
        colls = new Collider[MAX_COLLS];

        base.InitData();
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
    
}