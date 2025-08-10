using UnityEngine;

public abstract class Actions : ScriptableObject
{
    public abstract void Init();
    public abstract void Act(BotAIController controller);
}