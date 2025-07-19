using UnityEngine;

[RequireComponent(typeof(ResCollector))]
public abstract class Entity : MonoBehaviour
{
    public abstract Vector3 Position { get; }
    public abstract Vector3 Velocity { get; }
}