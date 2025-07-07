using UnityEngine;

public abstract class SwitchAction : ScriptableObject
{
    public abstract void Init();
    public abstract bool SwitchAct(BotAIController controller);
}