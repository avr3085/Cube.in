using UnityEngine;

[CreateAssetMenu(fileName = "CombatSwitch", menuName = "AI/Transit/CombatSwitch")]
public class CombatSwitchAction : SwitchAction
{
    [Header("Average Chase Time")]
    [Range(1, 10)] public int minTime = 5;
    [Range(1, 20)] public int maxTime = 15;

    private float chaseTime;
    private bool stayInState;

    public override bool StayInState => stayInState;

    public override void Init()
    {
        stayInState = true;
        chaseTime = Random.Range(minTime, maxTime);
    }

    public override bool SwitchAct(BotAIController controller)
    {
        return CombatSwitch(controller);
    }

    private bool CombatSwitch(BotAIController controller)
    {
        // true state - patrol state
        // false state - survival state

        /**
         if plyaer is too far switch to roaming state
         if player time is over switch to patrol state
         if health is low, switch to survival state
        */

        if (controller.HasCriticalHealth)
        {
            // switch to survival state
            controller.hasCombatTarget = false;
            controller.comabtTarget = null;
            stayInState = false;
            return false;
        }

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