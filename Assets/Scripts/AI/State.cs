using UnityEngine;

/// <summary>
/// FSM system
/// </summary>
[CreateAssetMenu(fileName = "State", menuName = "AI/State")]
public class State : ScriptableObject
{
    public Actions act = default;
    public Transitions transAct = default;

    public void Init()
    {
        act.Init();
        transAct.actions.Init();
    }

    public void UpdateState(BotAIController controller)
    {
        ExecuteActions(controller);
        ExecuteTransitActions(controller);
    }

    private void ExecuteActions(BotAIController controller)
    {
        act.Act(controller);
    }

    private void ExecuteTransitActions(BotAIController controller)
    {
        if (transAct.actions.SwitchAct(controller))
        {
            controller.SwitchState(transAct.trueState);
        }
        else
        {
            if (!transAct.actions.StayInState)
            {
                controller.SwitchState(transAct.falseState);
            }
        }
    }

}