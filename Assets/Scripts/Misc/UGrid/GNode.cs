using UnityEngine;

public class GNode
{
    public int key;
    public Vector2 position;
    public int matrixID;
    public int parent;
    public int next;

    public GNode(int key, Vector2 position, int matrixID)
    {
        this.key = key;
        this.position = position;
        this.matrixID = matrixID;
        parent = -1;
        next = -1;
    }
}