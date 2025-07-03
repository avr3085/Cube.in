using UnityEngine;

[CreateAssetMenu(fileName = "State", menuName = "AI/State")]
public class State : ScriptableObject
{
    public Actions[] actions = default;

    public void UpdateState(BotAIController controller)
    {
        ExecuteActions(controller);
    }

    private void ExecuteActions(BotAIController controller)
    {
        foreach (Actions act in actions)
        {
            act.Act(controller);
        }
    }
}