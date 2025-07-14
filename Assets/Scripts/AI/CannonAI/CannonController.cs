using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private int rotationSpeed = 5;
    [SerializeField] private BotAIController botAIController;
    [SerializeField, Range(1f, 5f)] private float waitTime = 1f;

    private float elapsedTime;
    private Vector3 lookDirection;
    private const float yAxisLevel = 0.5f;
    private void Start()
    {
        lookDirection = transform.forward;
        lookDirection.y += yAxisLevel;
        elapsedTime = 0f;
    }

    private void Update()
    {
        if (elapsedTime < waitTime)
        {
            elapsedTime += Time.deltaTime;
        }
        else
        {
            int res = botAIController.CheckOverlapsBox();
            if (res > 1)
            {
                foreach (Collider col in botAIController.Colls)
                {
                    var item = col.GetComponent<Entity>();
                    if (item != botAIController)
                    {
                        lookDirection = item.Position - transform.position;
                        lookDirection.y += yAxisLevel;
                        break;
                    }
                }
            }

            elapsedTime = 0f;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), rotationSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + lookDirection, 0.2f);
    }
}