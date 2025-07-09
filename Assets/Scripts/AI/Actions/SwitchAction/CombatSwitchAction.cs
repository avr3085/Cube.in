using UnityEngine;

[CreateAssetMenu(fileName = "CombatSwitch", menuName = "AI/Transit/CombatSwitch")]
public class CombatSwitchAction : SwitchAction
{
    [Header("Average Chase Time")]
    [Range(1, 10)] public int minTime = 5;
    [Range(1, 20)] public int maxTime = 15;

    private float chaseTime;

    public override void Init()
    {
        chaseTime = Random.Range(minTime, maxTime);
    }

    public override bool SwitchAct(BotAIController controller)
    {
        return CombatSwitch(controller);
    }

    private bool CombatSwitch(BotAIController controller)
    {
        // if health is health is low return false;
        // if the target is null, then return false

        if ((controller.Position - controller.comabtTarget.Position).sqrMagnitude > controller.Stats.ChaseTargeDistSqrd)
        {
            controller.comabtTarget = null;
            return true;
        }

        if (chaseTime > 0f)
        {
            chaseTime -= Time.deltaTime;
        }
        else
        {
            controller.comabtTarget = null;
            return true;
        }

        return false;
    }
}