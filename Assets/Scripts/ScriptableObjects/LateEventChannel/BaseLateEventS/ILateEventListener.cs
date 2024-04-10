public interface ILateEventListener<T>
{
    void Enqueue(T arg);
    void Dequeue();
}

public interface ILateEventListener<T, P>
{
    void Enqueue(T arg0, P arg1);
    void Dequeue();
}