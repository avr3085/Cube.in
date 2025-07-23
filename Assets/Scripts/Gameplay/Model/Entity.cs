using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ResCollector))]
public abstract class Entity : MonoBehaviour, IDamage
{
    public UnityAction OnEventRaised;
    public abstract Vector3 Position { get; }
    public abstract Vector3 Velocity { get; }
    public abstract Collider[] Colls { get; }
    public abstract Rigidbody RBody { get; }
    public abstract int Health { get; }
    public abstract int Score { get; set;}

    public abstract int CheckOverlapsBox();
    public abstract void TakeDamage(int amount);
}