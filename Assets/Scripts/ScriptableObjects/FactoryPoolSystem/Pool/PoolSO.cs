using System.Collections.Generic;
using UnityEngine;

public abstract class PoolSO<T> : ScriptableObject, IPool<T>
{
    protected Stack<T> Available = new Stack<T>();
    protected abstract IFactory<T> Factory {get; set;}
    private bool hasPreWarmed = false;

    public virtual T Create()
    {
        return Factory.Create();
    }

    public virtual void PreWarm(int num)
    {
        if(hasPreWarmed)
        {
            return;
        }

        for(int i = 0; i<num; i++)
        {
            Available.Push(Create());
        }
        hasPreWarmed = true;
    }

    public virtual T Request()
    {
       return Available.Count > 0 ? Available.Pop() : Create(); 
    }

    public virtual void Return(T member)
    {
        Available.Push(member);
    }

    public virtual void Disable()
    {
        Available.Clear();
        hasPreWarmed = false;
    }
}