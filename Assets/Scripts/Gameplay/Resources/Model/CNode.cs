using UnityEngine;

public class CNode
{
    public Vector3 position;
    public Quaternion rotation;
    public CNode parent;
    public CNode next;
    public Matrix4x4 matrix;
    public bool shouldMove;
    public bool remove;
    public float t;

    public CNode(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
        matrix = Matrix4x4.TRS(position, rotation, Vector3.one);
        shouldMove = false;
        remove = false;
        t = 0;
    }

    public void MoveUp(float val)
    {
        position = new Vector3(this.position.x, this.position.y + val, this.position.z);
        // position = newpos;
        matrix = Matrix4x4.TRS(position, rotation, Vector3.one);

        if(t >= 1f)
        {
            shouldMove = false;
            remove = true;
        }
    }
}