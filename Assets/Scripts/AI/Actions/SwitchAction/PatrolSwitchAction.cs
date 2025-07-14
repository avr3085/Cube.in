using UnityEngine;

[CreateAssetMenu(fileName = "PatrolSwitch", menuName = "AI/Transit/PatrolSwitch")]
public class PatrolSwitchAction : SwitchAction
{
    // [Range(1, 5)] public int halfRadius = 1;
    // public LayerMask lMask = default;

    // private const int MAX_COLLS = 5;
    // private Collider[] colls;

    public override void Init()
    {
        // colls = new Collider[MAX_COLLS];
    }

    public override bool SwitchAct(BotAIController controller)
    {
        return PatrolSwitch(controller);
    }

    private bool PatrolSwitch(BotAIController controller)
    {
        if (controller.hasCombatTarget) return false;

        // int resCount = Physics.OverlapBoxNonAlloc(controller.Position, Vector3.one * halfRadius, colls, Quaternion.identity, lMask);
        int resCount = controller.CheckOverlapsBox();
        if (resCount > 1)
        {
            // if health is still low, switch to survival

            foreach (var c in controller.Colls)
            {
                Entity entity = c.GetComponent<Entity>();
                if (entity != null && entity != controller)
                {
                    controller.comabtTarget = entity;
                    controller.hasCombatTarget = true;
                    return true;
                }
            }
        }
        return false;
    }
}