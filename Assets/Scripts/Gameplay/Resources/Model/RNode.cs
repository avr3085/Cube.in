using UnityEngine;

/// <summary>
/// SpawnAnimation - Resource will show animation when spawned
/// Idol - Resting state
/// CollisionAnimation - state after the resource has collided to the player
/// Removal - This item will get removed permanently
/// </summary>
public enum RNodeState
{
    SpawnAnimation,
    Idol,
    CollisionAnimation,
    Removal
}

/// <summary>
/// RNode
/// key = grid cell id, which this cell belongs to
/// t = animation evaluation time
/// position = position of the particle in the world space
/// roatation = rotation matrix
/// matrix = TRS matrix for the GPU
/// isMoving = if the particle is moving in space
/// hasMoved = if the particle has already moved then we will remove it from the index
/// </summary>

public class RNode
{
    public int key;
    public float moveT;
    public float scaleT;
    public Vector3 position;
    public Quaternion rotation;
    public Matrix4x4 matrix;
    public RNodeState state;
    public RNode(int key, Vector3 position, Quaternion rotation)
    {
        this.key = key;
        this.position = position;
        this.rotation = rotation;
        moveT = 0f;
        scaleT = 0f;
        matrix = Matrix4x4.TRS(position, rotation, Vector3.zero);
        state = RNodeState.SpawnAnimation;
    }

    public void EvaluateAnimation(float val)
    {
        switch(state)
        {
            case RNodeState.SpawnAnimation:
                SpawnAnimation(val);
                break;

            case RNodeState.CollisionAnimation:
                CollisionAnimation(val);
                break;

            default:
                Debug.LogWarning("Undefined state");
                break;
        }
    }

    private void SpawnAnimation(float val)
    {
        matrix = Matrix4x4.TRS(position, rotation, Vector3.one * val);
        if(scaleT >= 1f)
        {
            state = RNodeState.Idol;
        }
    }

    private void CollisionAnimation(float val)
    {
        position.y += val;
        matrix = Matrix4x4.TRS(position, rotation, Vector3.one);
        if(moveT >= 1f)
        {
            state = RNodeState.Removal;
        }
    }
}

/// <summary>
/// HNode, For Hash Map
/// </summary>
public class HNode
{
    public int startIndex;
    public int activeNodes;
    public int totalNodes;

    public HNode(int startIndex)
    {
        this.startIndex = startIndex;
        activeNodes = 1;
        totalNodes = 1;
    }
}