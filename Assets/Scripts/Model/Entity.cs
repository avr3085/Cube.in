using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public abstract Vector3 Position { get;}
    public abstract Vector3 Velocity { get;}
}