using UnityEngine;

/// <summary>
/// Player Movement and rotation handler
/// </summary>
[RequireComponent(typeof(PlayerCustomization))]
public class PlayerMovement : EntityData
{
    [Header("Player Properties")]
    [SerializeField, Range(1, 10)] private int moveSpeed = 1;
    [SerializeField, Range(1, 100)] private int rotationSpeed = 50;

    [Header("Overlap Test")]
    [Range(1, 10), Tooltip("This value should match as the Bot Stats SO half Radius.")]
    public int halfRadius = 1;
    public LayerMask lMask;

    [Header("Data Channel"), SerializeField] private AudioSO EntityExplosionAudioSO = default;

    [Space(10), Header("Listening Channel"), SerializeField] private Vector2EventListener inputAxisListener = default;

    [Header("Broadcasting Channel")]
    [SerializeField] private FXRequestHandler explosionFXRequest = default;
    [SerializeField] private AudioClipRequestHandler audioClipRequestHandler = default;
    [SerializeField] private VoidEventListener gameOverRequestHandler = default;

    private Vector3 rotationDirection;

    private void OnEnable()
    {
        inputAxisListener.onEventRaised += RotatePlayer;
    }

    private void OnDisable()
    {
        inputAxisListener.onEventRaised -= RotatePlayer;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        colls = new Collider[MAX_COLLS];

        base.InitData();
    }

    /// <summary>
    /// Rotating Rigidbody toward the movement direction
    /// </summary>
    /// <param name="inputAxis"></param>
    private void RotatePlayer(Vector2 inputAxis)
    {
        float angle = Mathf.Atan2(inputAxis.x, inputAxis.y) * Mathf.Rad2Deg;
        rotationDirection = new Vector3(0f, angle, 0f);
    }

    private void FixedUpdate()
    {
        velocity = rb.velocity;
        velocity = transform.forward * moveSpeed;
        rb.velocity = velocity;

        // rb.rotation = Quaternion.Lerp(rb.rotation, Quaternion.Euler(rotationDirection), Time.fixedDeltaTime * rotationSpeed);
        var rot = Quaternion.Slerp(rb.rotation, Quaternion.Euler(rotationDirection), Time.fixedDeltaTime * rotationSpeed);
        rb.MoveRotation(rot);
    }

    public override int CheckOverlapsBox()
    {
        return Physics.OverlapBoxNonAlloc(Position, Vector3.one * halfRadius, colls, Quaternion.identity, lMask);
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);

        // if player health is less than zero. it is a game over
        if (health <= 0)
        {
            // pop game over UI and finish the game
            explosionFXRequest.Raise(Position);
            audioClipRequestHandler.Raise(EntityExplosionAudioSO);
            gameOverRequestHandler.Raise(); // Pops up the game over UI
            gameObject.SetActive(false);
        }
    }
}