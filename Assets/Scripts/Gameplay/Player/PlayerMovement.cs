using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Properties")]
    [SerializeField, Range(1,10)] private int moveSpeed = 1;
    [SerializeField, Range(1,100)] private int rotationSpeed = 50;
    [Space(10), Header("Broadcasting Channel"), SerializeField] private Vector3EventListener targetPositionListener = default;

    [Space(10), Header("Listening Channel"), SerializeField] private IntEventListener inputAxisListener = default;

    private void OnEnable()
    {
        inputAxisListener.onEventRaised += RotatePlayer;
    }

    private void OnDisable()
    {
        inputAxisListener.onEventRaised -= RotatePlayer;
    }

    private void RotatePlayer(int inputAxis)
    {
        //rotate player to a degree

        //rotation in 3d world
        transform.Rotate(new Vector3(0f, inputAxis * rotationSpeed * Time.deltaTime, 0f));
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        // targetPositionListener.Raise(transform.position);
    }
    
    private void LateUpdate()
    {
        //move player to the right Direction
        targetPositionListener.Raise(transform.position);
    }
}