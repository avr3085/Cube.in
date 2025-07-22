using UnityEngine;

[RequireComponent(typeof(ResCollector))]
public abstract class Entity : MonoBehaviour, IDamage
{
    public abstract int CheckOverlapsBox();
    public abstract void TakeDamage(int amount);
    
    public abstract Vector3 Position { get; }
    public abstract Vector3 Velocity { get; }
    public abstract Collider[] Colls { get; }
    public abstract Rigidbody RBody { get; }
}