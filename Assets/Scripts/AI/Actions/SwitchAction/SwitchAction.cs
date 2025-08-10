using UnityEngine;

public abstract class SwitchAction : ScriptableObject
{
    public abstract bool StayInState { get; }
    public abstract void Init();
    public abstract bool SwitchAct(BotAIController controller);
}