using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseLateEventSO<T> : ScriptableObject, ILateEventListener<T>
{
    protected Queue<T> NotificationStack = new Queue<T>();
    public UnityAction<T> OnEventRaised;
    
    public virtual void Enqueue(T member)
    {
        NotificationStack.Enqueue(member);
    }

    public virtual void Dequeue()
    {
        // if(NotificationStack.Count > 0)
        // {
        //     return NotificationStack.Dequeue();
        // }
        // return default(T);

        if(NotificationStack.Count > 0)
        {
            OnEventRaised?.Invoke(NotificationStack.Dequeue());
        }
        else
        {
            OnEventRaised?.Invoke(default(T));
        }
    }
}