using UnityEngine;

[CreateAssetMenu(fileName = "PatrolSwitch", menuName = "AI/Transit/PatrolSwitch")]
public class PatrolSwitchAction : SwitchAction
{
    private bool stayInState;
    public override bool StayInState => stayInState;

    public override void Init()
    {
        stayInState = true;
    }

    public override bool SwitchAct(BotAIController controller)
    {
        return PatrolSwitch(controller);
    }

    private bool PatrolSwitch(BotAIController controller)
    {
        int resCount = controller.CheckOverlapsBox();
        if (resCount > 1)
        {
            if (controller.HasCriticalHealth)
            {
                stayInState = false;
                controller.hasPatrolPoint = false;
                return false;
            }

            foreach (var c in controller.Colls)
            {
                Entity entity = c.GetComponent<Entity>();
                if (entity != null && entity != controller)
                {
                    controller.hasPatrolPoint = false;
                    controller.comabtTarget = entity;
                    controller.hasCombatTarget = true;
                    return true;
                }
            }
        }
        return false;
    }
}