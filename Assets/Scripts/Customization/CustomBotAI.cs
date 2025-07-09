using UnityEngine;

public class CustomBotAI : MonoBehaviour
{
    [SerializeField, Range(5, 20)] private int roamAreaSize = 10;
    [SerializeField, Range(1, 10)] private int moveSpeed = 6;
    [SerializeField, Range(1, 100)] private int rotationSpeed = 10;

    private Vector3 randPosition;
    private Vector3 moveDirection;

    private void Start()
    {
        randPosition = new Vector3(Random.Range(-roamAreaSize, roamAreaSize), 0f, Random.Range(-roamAreaSize, roamAreaSize));
        moveDirection = (randPosition - transform.position).normalized;
    }

    private void Update()
    {
        MoveCustomBot();
    }

    private void MoveCustomBot()
    {
        if (Vector3.Distance(transform.position, randPosition) < 0.5f)
        {
            randPosition = new Vector3(Random.Range(-roamAreaSize, roamAreaSize), 0f, Random.Range(-roamAreaSize, roamAreaSize));
            moveDirection = (randPosition - transform.position).normalized;
        }
        else
        {
            transform.position += moveDirection * (moveSpeed * Time.deltaTime);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed * Time.deltaTime);
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawSphere(randPosition, 0.1f);
    // }
}