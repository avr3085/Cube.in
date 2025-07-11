using UnityEngine;
using Misc;

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

    private Rigidbody rb;
    private Vector3 velocity;

    public override Vector3 Position => new Vector3(transform.position.x, 0f, transform.position.z);
    public override Vector3 Velocity => velocity;
    public float RotAngle => Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
    public BotStats Stats => botStats;
    public Vector3 SetVelocity
    {
        set => velocity = value;
    }

    public Vector3 SetPosition
    {
        set => transform.position = value;
    }

    public Vector3 UpdateRotVector() => rotVector = new Vector3(0f, RotAngle, 0f);

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocity = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        rotVector = new Vector3(0f, RotAngle, 0f);

        currentState.Init();
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity * botStats.moveSpeed;
        var rot = Quaternion.Slerp(rb.rotation, Quaternion.Euler(rotVector), Time.fixedDeltaTime * botStats.rotationSpeed);
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
}