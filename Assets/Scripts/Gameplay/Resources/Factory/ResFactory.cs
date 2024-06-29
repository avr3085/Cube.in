using System.Collections.Generic;
using UnityEngine;
using MiscUtils;

public abstract class ResFactory : ScriptableObject, IRes
{
    private int extraSpace;
    private int entityCount;
    private int mapSize;

    protected abstract ResConfig ResConfig{get;}
    protected abstract bool IsMultiColor{get;}
    protected Matrix4x4[] positionMatrix;
    protected List<CNode> collisionNode;
    protected Dictionary<int, CNode> map;
    protected int nodeCount = 0;

    // public int NodeCount => nodeCount;
    // public Matrix4x4[] PositionMatrix => positionMatrix;
    // public List<CNode> CollisionNode => collisionNode;
    // public Dictionary<int, CNode> Map => map;

    //MultiColor
    private Vector4[] colors;
    protected MaterialPropertyBlock block;

    //Command Queue
    private readonly Queue<int> queue = new Queue<int>();
    private bool isProcessing = false;

    public virtual void Init()
    {
        this.extraSpace = ResConfig.extraSpace;
        this.entityCount = ResConfig.entityCount + extraSpace;
        this.mapSize = ResConfig.mapSize;

        positionMatrix = new Matrix4x4[entityCount];
        collisionNode = new List<CNode>();
        map = new Dictionary<int, CNode>();

        colors = new Vector4[entityCount];
        block = new MaterialPropertyBlock();

        GenerateRes(entityCount);
    }

    private void GenerateRes(int count)
    {
        if(IsMultiColor)
        {
            for(int i = 0; i < count; i++)
            {
                colors[i] = Color.Lerp(Color.red, Color.blue, Random.value);
            }

            block.SetVectorArray("_Colors", colors);
        }

        for(int i = 0; i < count; i++)
        {
            FillItem();
        }
    }

    private void FillItem()
    {
        float floatMapSize = mapSize; 
        var randPos = new Vector3(Random.Range(-floatMapSize, floatMapSize), 0f, Random.Range(-floatMapSize, floatMapSize));
        Quaternion randRot = Quaternion.Euler(0f, Random.Range(-180, 180), 0f);

        int hashKey = randPos.GetHash();
        var newNode = new CNode(randPos, randRot);
        if(!map.ContainsKey(hashKey))
        {
            map.Add(hashKey, newNode);
        }else
        {
            var node = map[hashKey];
            node.next = newNode;
            newNode.parent = node;
            map[hashKey] = newNode;
        }
        collisionNode.Add(newNode);
        nodeCount += 1;
    }

    public virtual void AddItem()
    {
        FillItem();
    }

    public virtual void RemoveItem(int hashKey)
    {
        queue.Enqueue(hashKey);
        if(!isProcessing)
        {
            isProcessing = true;
            DoNext();
        }
    }

    private void DoNext()
    {
        if(queue.Count == 0)
        {
            isProcessing = false;
            return;
        }

        var hashKey = queue.Dequeue();
        var node = map[hashKey];
        while(node != null)
        {
            var nextNode = node.parent;
            node.shouldMove = true;
            node = nextNode;
        }
        map.Remove(hashKey);
        DoNext();
    }


    public virtual void DeInit()
    {
        nodeCount = 0;
        map.Clear();
    }
}