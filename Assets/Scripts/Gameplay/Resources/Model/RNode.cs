using UnityEngine;

/// <summary>
/// RNode
/// hashId = grid cell id, which this cell belongs to
/// t = animation evaluation time
/// position = position of the particle in the world space
/// roatation = rotation matrix
/// matrix = TRS matrix for the GPU
/// isMoving = if the particle is moving in space
/// hasMoved = if the particle has already moved then we will remove it from the index
/// </summary>

public class RNode
{
    public int hashKey;
    public float t;
    public Vector3 position;
    public Quaternion rotation;
    public Matrix4x4 matrix;
    public bool animate;
    public bool hasAnimated;

    public RNode(int hashKey, Vector3 position, Quaternion rotation)
    {
        this.hashKey = hashKey;
        this.position = position;
        this.rotation = rotation;
        matrix = Matrix4x4.TRS(position, rotation, Vector3.one);
        animate = false;
        hasAnimated = false;
        t = 0f;
    }

    public void MoveUp(float posyVal)
    {
        position = new Vector3(this.position.x, this.position.y + posyVal, this.position.z);
        matrix = Matrix4x4.TRS(position, rotation, Vector3.one);
        if(t >= 1f)
        {
            animate = false;
            hasAnimated = true;
        }
    }
}

/// <summary>
/// HNode, For Hash Map
/// </summary>
public class HNode
{
    public int startIndex;
    public int nodeCount;
    public int totalNode;

    public HNode(int startIndex)
    {
        this.startIndex = startIndex;
        nodeCount = 1;
        totalNode = 1;
    }
}