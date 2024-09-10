using MiscUtils;
using UnityEngine;

public class BotAI : MonoBehaviour
{
    [Header("Bot Properties")]
    [SerializeField, Range(1,10)] private int moveSpeed = 1;
    [SerializeField, Range(1,100)] private int rotationSpeed = 50;
    [SerializeField, Range(1, 100)] private float boundry = 48f;
    private float turn = 0.1f;

    private Rigidbody rb;
    private Vector3 rbVelocity, rotationAngle;
    private Vector2 moveVelocity;


    public Vector2 Position => new Vector2(transform.position.x, transform.position.z);
    public float Angle => Mathf.Atan2(moveVelocity.x, moveVelocity.y) * Mathf.Rad2Deg;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveVelocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rotationAngle = new Vector3(0f, Angle, 0f);
    }

    private void Update()
    {
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

    private void WrapAround()
    {
        Vector2 currentVelocity = moveVelocity;
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

        Vector2 newDir = moveVelocity - currentVelocity;
        moveVelocity -= newDir.normalized;

        rotationAngle = new Vector3(0f, Angle, 0f);
    }

}