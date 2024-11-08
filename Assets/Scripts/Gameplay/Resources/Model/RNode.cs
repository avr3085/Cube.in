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
/// scaleT = animates the size of the resource
/// moveT = animates the resource when collision occurs
/// position = position of the particle in the world space
/// roatation = rotation matrix
/// matrix = TRS matrix for the GPU
/// node = current state of the node
/// animatingAlready = tracks if node is already in the animation list, [this will prevent duplication in the list, and outofIndex error]
/// </summary>

public class RNode
{
    public int key;
    public float scaleT;
    public float moveT;
    public Vector3 position;
    public Quaternion rotation;
    public Matrix4x4 matrix;
    public RNodeState state;
    public bool animatingAlready;

    public RNode(int key, Vector3 position, Quaternion rotation, bool animatingAlready = false)
    {
        this.key = key;
        this.position = position;
        this.rotation = rotation;
        scaleT = 0f;
        moveT = 0f;
        matrix = Matrix4x4.TRS(position, rotation, Vector3.zero);
        state = RNodeState.SpawnAnimation;
        this.animatingAlready = animatingAlready;
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
    public int index;
    public int totalNodes;

    public HNode(int index)
    {
        this.index = index;
        totalNodes = 1;
    }
}