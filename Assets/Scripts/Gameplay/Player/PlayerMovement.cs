using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Properties")]
    [SerializeField, Range(1,10)] private int moveSpeed = 1;
    [SerializeField, Range(1,100)] private int rotationSpeed = 50;

    [SerializeField] private Transform ball = default;
    [SerializeField, Min(0.1f)] private float ballRadius = 0.5f;

    [Space(10), Header("Broadcasting Channel"), SerializeField] private Vector3EventListener targetPositionListener = default;

    [Space(10), Header("Listening Channel"), SerializeField] private Vector2EventListener inputAxisListener = default;

    private Rigidbody rb;
    private Vector3 velocity;

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
    }

    private void RotatePlayer(Vector2 inputAxis)
    {
        // float angle = Mathf.Atan2(inputAxis.y, inputAxis.x) * Mathf.Rad2Deg;
        // Quaternion desiredAngle = Quaternion.Euler(0f, angle, 0f);
        // float deltaTime = rotationSpeed * Time.deltaTime;

        // transform.rotation = Quaternion.Lerp(transform.rotation, desiredAngle, deltaTime);
    }

    // private void Update()
    // {
    //     Vector3 movement = rb.velocity * Time.deltaTime;
    //     float distance = movement.magnitude;
    //     float angle = distance * (180f / Mathf.PI) / ballRadius;
    //     ball.localRotation = Quaternion.Euler(Vector3.right * angle) * ball.rotation;
    // }

    private void FixedUpdate()
    {
        velocity = transform.forward * moveSpeed;
        //add acceleration
        rb.velocity = velocity;
    }
    
    private void LateUpdate()
    {
        //move player to the right Direction
        targetPositionListener.Raise(transform.position);
    }
}