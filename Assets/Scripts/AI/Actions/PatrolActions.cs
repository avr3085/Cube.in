using UnityEngine;
using System.Collections.Generic;
using Misc;

[CreateAssetMenu(fileName = "PatrolAction", menuName = "AI/Action/Patrol")]
public class PatrolActions : Actions
{
    private int currentHash;
    private IEnumerable<int> hashArray;

    public override void Init()
    {
        currentHash = -1;
    }

    public override void Act(BotAIController controller)
    {
        Patrol(controller);
    }

    private void Patrol(BotAIController controller)
    {
        if (controller.hasPatrolPoint)
        {
            float patrolOffset = (controller.patrolPoint - controller.Position).sqrMagnitude;
            if (patrolOffset < 1f || patrolOffset > controller.Stats.visionRange * controller.Stats.visionRange)
            {
                controller.hasPatrolPoint = false;
            }
            return;
        }

        Vector3 pPos = new Vector3(controller.Position.x - 0.5f, 0f, controller.Position.z - 0.5f);

        if (currentHash == pPos.ToHash()) return;

        currentHash = pPos.ToHash();
        hashArray = controller.Position.BoxVisionHash(controller.Stats.visionRange);

        if (hashArray == null) return;

        Vector3 nearestTarget;
        foreach (var hashKey in hashArray)
        {
            if (ResFactoryManager.Instance.ContainsKey(hashKey))
            {
                nearestTarget = ResFactoryManager.Instance.NearestRes(hashKey, controller.Position);
                if (!nearestTarget.Equals(Vector3.zero))
                {
                    float distSqrd = (nearestTarget - controller.Position).sqrMagnitude;
                    if (distSqrd < controller.Stats.visionRange * controller.Stats.visionRange && distSqrd > 1f)
                    {
                        controller.patrolPoint = nearestTarget;
                        controller.hasPatrolPoint = true;

                        Vector3 newDir = controller.Velocity + (controller.patrolPoint - controller.Position);
                        controller.SetVelocity = newDir.normalized;
                        controller.UpdateRotVector();
                        break;
                    }
                }
            }
        }
    }
}