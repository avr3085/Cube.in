using UnityEngine;

[CreateAssetMenu(fileName = "SurvivalSwitch", menuName = "AI/Transit/SurvivalSwitch")]
public class SurvivalSwitchAction : SwitchAction
{
    private bool stayInState;
    private float elapesdTime;
    private const float waitTime = 10f;
    public override bool StayInState => stayInState;

    public override void Init()
    {
        elapesdTime = 0f;
        stayInState = true;
    }

    public override bool SwitchAct(BotAIController controller)
    {
        return SurvivalSwitch(controller);
    }

    private bool SurvivalSwitch(BotAIController controller)
    {
        // True state = patrol state
        // false state = comabt state
        if (elapesdTime < waitTime)
        {
            elapesdTime += Time.deltaTime;
        }
        else
        {
            int resCount = controller.CheckOverlapsBox();
            if (resCount > 1 && !controller.HasCriticalHealth)
            {
                foreach (var c in controller.Colls)
                {
                    Entity entity = c.GetComponent<Entity>();
                    if (entity != null && entity != controller)
                    {
                        controller.hasCombatTarget = true;
                        controller.comabtTarget = entity;
                        stayInState = false;
                        return false;
                    }
                }
            }
            
            return true;
        }
        return false;
    }
}