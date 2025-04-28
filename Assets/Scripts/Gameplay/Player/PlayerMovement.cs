using UnityEngine;

/// <summary>
/// Player Movement and rotation handler
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [Header("Player Properties")]
    [SerializeField, Range(1,10)] private int moveSpeed = 1;
    [SerializeField, Range(1,100)] private int rotationSpeed = 50;

    [Space(10), Header("Listening Channel"), SerializeField] private Vector2EventListener inputAxisListener = default;

    private Rigidbody rb;
    private Vector3 velocity, rotationDirection;

    // Collider properties
    // private int maxColliders = 10;
    // private Collider[] hitColliders;

    private void OnEnable()
    {
        inputAxisListener.onEventRaised += RotatePlayer;
    }

    private void OnDisable()
    {
        inputAxisListener.onEventRaised -= RotatePlayer;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        // hitColliders = new Collider[maxColliders];
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

    private void Update()
    {
        // int hitCount = Physics.OverlapBoxNonAlloc(transform.position, Vector3.one * 2f, hitColliders);
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
}