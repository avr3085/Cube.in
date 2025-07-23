using UnityEngine;

public abstract class EntityData : Entity
{
    protected int health;
    protected int score;
    protected MissileType mType;
    protected int level;
    protected Rigidbody rb;
    protected Vector3 velocity;
    protected Collider[] colls;
    protected const int MAX_COLLS = 5;

    public override int Health => health;
    public override Rigidbody RBody => rb;
    public override Collider[] Colls => colls;
    public override Vector3 Position => new Vector3(transform.position.x, 0f, transform.position.z);
    public override Vector3 Velocity => velocity;
    public override int Score
    {
        get => score;
        set
        {
            score = value;
        }
    }

    public virtual void InitData()
    {
        score = 0;
        health = 100;
        mType = MissileType.Missile;
    }

    public virtual void InitData(MissileType misType)
    {
        score = 0;
        health = 100;
        mType = misType;
    }

    public override void TakeDamage(int amount)
    {
        health -= amount;
        OnEventRaised?.Invoke();
    }
}