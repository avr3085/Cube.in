/// <summary>
/// Transition From one state to another
/// </summary>
[System.Serializable]
public class Transitions
{
    public SwitchAction actions;
    public State trueState;
    public State falseState;
}