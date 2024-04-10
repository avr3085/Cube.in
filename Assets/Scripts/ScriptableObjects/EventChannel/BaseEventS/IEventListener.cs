public interface IEventListener<T>
{
    void Raise(T arg);
}

public interface IEventListener<T, P>
{
    void Raise(T arg0, P arg1);
}
