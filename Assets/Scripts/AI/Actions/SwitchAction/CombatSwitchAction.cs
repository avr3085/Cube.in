using UnityEngine;

[CreateAssetMenu(fileName = "CombatSwitch", menuName = "AI/Transit/CombatSwitch")]
public class CombatSwitchAction : SwitchAction
{
    [Range(1, 5)] public int halfRadius = 1;
    public LayerMask lMask = default;

    private const int MAX_COLLS = 5;
    private Collider[] colls;

    public override void Init()
    {
        colls = new Collider[MAX_COLLS];
    }

    public override bool SwitchAct(BotAIController controller)
    {
        return CombatSwitch(controller);
    }

    private bool CombatSwitch(BotAIController controller)
    {
        if (controller.hasCombatTarget) return false;

        int resCount = Physics.OverlapBoxNonAlloc(controller.Position, Vector3.one * halfRadius, colls, Quaternion.identity, lMask);
        if (resCount > 1)
        {
            foreach (var c in colls)
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