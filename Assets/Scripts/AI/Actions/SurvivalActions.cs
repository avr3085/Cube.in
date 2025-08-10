using UnityEngine;

/// <summary>
/// Actions performed in the survival state
/// </summary>
[CreateAssetMenu(fileName = "SurvivalAction", menuName = "AI/Action/Survival")]
public class SurvivalActions : Actions
{
    // [Range(1, 5)] public int halfRadius = 1;
    [Range(0.1f, 5f)] public float waitTimer = 0.5f;
    // public LayerMask lMask = default;

    // private const int MAX_COLLS = 5;
    // private Collider[] colls;
    private float elapesdTime;

    public override void Init()
    {
        // colls = new Collider[MAX_COLLS];
        elapesdTime = 0f;
    }

    public override void Act(BotAIController controller)
    {
        Survive(controller);
    }

    private void Survive(BotAIController controller)
    {
        if (elapesdTime < waitTimer)
        {
            elapesdTime += Time.deltaTime;
        }
        else
        {
            Vector3 moveDirection = Vector3.zero;
            // int resCount = Physics.OverlapBoxNonAlloc(controller.Position, Vector3.one * halfRadius, colls, Quaternion.identity, lMask);
            int resCount = controller.CheckOverlapsBox();
            if (resCount > 1)
            {
                foreach (var c in controller.Colls)
                {
                    Entity entity = c.GetComponent<Entity>();
                    if (entity != null && entity != controller)
                    {
                        moveDirection += controller.Position - entity.Position;
                        break;
                    }
                }
            }

            Vector3 newDir = controller.Velocity + moveDirection;
            controller.SetVelocity = newDir.normalized;
            controller.UpdateRotVector();
        }
    }
}