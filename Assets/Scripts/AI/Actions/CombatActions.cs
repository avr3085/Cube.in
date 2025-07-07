using UnityEngine;
using Misc;
using UnityEngine.U2D;

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
            float maxVal = -100f;
            // Vector3 interestDir = controller.transform.right;

            foreach (Vector3 sRay in sRays)
            {
                float dotProduct = HelperUtils.DotProduct(sRay, controller.comabtTarget.Position - controller.Position);
                if (Physics.Raycast(controller.Position, sRay, out hit, raySize, lMask))
                {
                    // Debug.DrawRay(controller.Position, hit.point - controller.Position, Color.green);
                    IDMap[index] = 0f;

                    int oppIndex = (index >= 4) ? index % 4 : 4 + index % 4;
                    IDMap[oppIndex] += Mathf.Abs(dotProduct);
                }
                else
                {
                    IDMap[index] = (dotProduct > 0) ? dotProduct : 0f;
                }

                if (IDMap[index] > maxVal)
                {
                    maxVal = IDMap[index];
                    interestDir = sRay;
                }

                Debug.DrawRay(controller.Position, sRay * IDMap[index], Color.red);
                index++;
            }


            Vector3 newDir = controller.Velocity + interestDir;
            controller.SetVelocity = newDir.normalized;
            interestDir = newDir.normalized;
            controller.UpdateRotVector();

            currWaitTime = 0f;
        }

        Debug.DrawRay(controller.Position, interestDir * 2f, Color.green);
    }
}