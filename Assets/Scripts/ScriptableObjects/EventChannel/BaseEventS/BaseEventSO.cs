using UnityEngine;
using UnityEngine.Events;

/*
* All Type of event Listener's should be inherited from this class.
* Only excemption is Void Event Listener.
*/
public abstract class BaseEventSO<T> : ScriptableObject, IEventListener<T>
{
    public string description;
    public UnityAction<T> onEventRaised;

    public virtual void Raise(T arg){
        onEventRaised?.Invoke(arg);
    }
}

public abstract class BaseEventSO<T,P> : ScriptableObject, IEventListener<T,P>
{
    public string description;
    public UnityAction<T,P> onEventRaised;

    public virtual void Raise(T arg0,P arg1){
        onEventRaised?.Invoke(arg0, arg1);
    }
}
