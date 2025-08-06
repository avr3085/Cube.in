using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Base entity stats component/interface, which will be inherited later to diffrent entities
/// </summary>
[RequireComponent(typeof(ResCollector))]
public abstract class Entity : MonoBehaviour, IData
{
    public UnityAction OnEventRaised;
    public abstract Vector3 Position { get; }
    public abstract Vector3 Velocity { get; }
    public abstract Collider[] Colls { get; }
    public abstract Rigidbody RBody { get; }
    public abstract int Health { get; }
    public abstract int Score { get; set; }
    public abstract int Level { get; }
    public abstract MissileType ActiveMissileType { get; }
    public abstract int MaxResTillUpdate { get; }
    public abstract int CurrResTillUpdate { get; }

    public abstract int CheckOverlapsBox();
    public abstract void TakeDamage(int amount);
    public abstract void AddScore(int amount);
}