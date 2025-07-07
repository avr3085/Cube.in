using UnityEngine;

[CreateAssetMenu(fileName = "State", menuName = "AI/State")]
public class State : ScriptableObject
{
    public Actions[] actions = default;
    public Transitions[] transitions = default;

    public void Init()
    {
        foreach (Actions act in actions)
        {
            act.Init();
        }

        foreach (Transitions t in transitions)
        {
            t.actions.Init();
        }
    }

    public void UpdateState(BotAIController controller)
    {
        ExecuteActions(controller);
        ExecuteTransitActions(controller);
    }

    private void ExecuteActions(BotAIController controller)
    {
        foreach (Actions act in actions)
        {
            act.Act(controller);
        }
    }

    private void ExecuteTransitActions(BotAIController controller)
    {
        foreach (Transitions t in transitions)
        {
            if (t.actions.SwitchAct(controller))
            {
                controller.SwitchState(t.trueState);
            }
            // add false state logic as well
        }
    }

}