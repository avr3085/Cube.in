using UnityEngine;
using Misc;

/// <summary>
/// Action to be performed in combat state
/// </summary>
// Note - The bot movement still feels like confusion, make sure the bot will act normal
[CreateAssetMenu(fileName = "CombatAction", menuName = "AI/Action/Combat")]
public class CombatActions : Actions
{
    [Range(1, 5)] public int raySize = 1;
    public LayerMask lMask = default;
    [Range(0.1f, 10f)] public float rayCastGap = 0.5f;

    private const int MAP_SIZE = 8;
    private float[] IDMap; // Interest and Danger map is included in the single array
    private float currWaitTime;
    private Vector3 interestDir;

    public override void Init()
    {
        IDMap = new float[MAP_SIZE];
        currWaitTime = 0f;
    }

    public override void Act(BotAIController controller)
    {
        Comabt(controller);
    }

    private void Comabt(BotAIController controller)
    {
        if (currWaitTime < rayCastGap)
        {
            currWaitTime += Time.deltaTime;
        }
        else
        {
            var sRays = controller.transform.GetSurroundRays();
            int index = 0;
            RaycastHit hit;
            float maxLen = -1000f;

            foreach (Vector3 sRay in sRays)
            {
                float dotProduct = HelperUtils.DotProduct(sRay, controller.comabtTarget.Position - controller.Position);
                IDMap[index] = (dotProduct > 0f) ? dotProduct : -dotProduct * 0.5f;

                if (Physics.Raycast(controller.Position, sRay, out hit, raySize, lMask))
                {
                    IDMap[index] = 0f;
                }

                if (IDMap[index] > maxLen)
                {
                    maxLen = IDMap[index];
                    interestDir = sRay;
                }

                // Debug.DrawRay(controller.Position, sRay * IDMap[index], Color.green);
                index++;
            }

            Vector3 newDir = controller.Velocity + interestDir;
            controller.SetVelocity = newDir.normalized;
            controller.UpdateRotVector();

            currWaitTime = 0f;
        }
        
        // Debug.DrawRay(controller.Position, interestDir * 5f, Color.red);
    }
}