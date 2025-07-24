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
    protected int tillUpCount;
    private const int FIX_MUL = 10;
    private const int FIX_ADD = 5;

    public override int Health => health;
    public override MissileType ActiveMissileType => mType;
    public override int MaxResTillUpdate =>  (FIX_MUL * (int)mType) + FIX_ADD;
    public override int CurrResTillUpdate => tillUpCount;
    public override int Level => level;
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
        level = 1;
        tillUpCount = MaxResTillUpdate;
    }

    public virtual void InitData(MissileType misType)
    {
        score = 0;
        health = 100;
        mType = misType;
        level = 1;
    }

    public override void TakeDamage(int amount)
    {
        health -= amount;
        OnEventRaised?.Invoke();
    }

    public override void AddScore(int amount)
    {
        score += amount * ((int)mType + 1); // since mType starts with 0
        if (health < 100) health += 5; /// kill the player here
        if (tillUpCount-- == 0 && level < 5)
        {
            mType++;
            level++;
            tillUpCount = MaxResTillUpdate;
        }
        
        OnEventRaised?.Invoke();
    }
}