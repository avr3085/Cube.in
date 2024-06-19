using System.Collections.Generic;
using UnityEngine;
using MiscUtils;

public abstract class ResFactory : ScriptableObject, IRes
{
    private int extraSpace;
    private int entityCount;
    private int mapSize;

    protected abstract ResConfig ResConfig{get;}
    protected Matrix4x4[] positionMatrix;
    protected CNode[] collisionNode;
    protected Dictionary<int, int> gridMap;

    private int freeNode = 0;
    private int nodeCount = 0;

    public int NodeCount => nodeCount;
    public Matrix4x4[] PositionMatrix => positionMatrix;
    public CNode[] CollisionNode => collisionNode;
    public Dictionary<int, int> GridMap => gridMap;

    public virtual void Init()
    {
        this.extraSpace = ResConfig.extraSpace;
        this.entityCount = ResConfig.entityCount + extraSpace;
        this.mapSize = ResConfig.mapSize;
        positionMatrix = new Matrix4x4[entityCount];
        collisionNode = new CNode[entityCount];
        gridMap = new Dictionary<int, int>();

        GenerateRes(entityCount);
    }

    private void GenerateRes(int count)
    {
        for(int i = 0; i < count; i++)
        {
            FillItem();
        }
    }

    private void FillItem()
    {
        float floatMapSize = mapSize; 
        var randPos = new Vector2(Random.Range(-floatMapSize, floatMapSize), Random.Range(-floatMapSize, floatMapSize));
        Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(-180, 180));
        positionMatrix[freeNode] = Matrix4x4.TRS(randPos, rotation, Vector3.one);

        int hashKey = randPos.GetHashKey(mapSize*2);
        collisionNode[freeNode] = new CNode(hashKey, randPos, freeNode);
        if(!gridMap.ContainsKey(hashKey))
        {
            gridMap.Add(hashKey, freeNode);
        }else
        {
            var indices = gridMap[hashKey];
            collisionNode[indices].next = freeNode;
            collisionNode[freeNode].parent = indices;
            gridMap[hashKey] = freeNode;
        }

        freeNode += 1;
        nodeCount += 1;
    }

    public virtual void AddItem()
    {
        FillItem();
    }

    public virtual void RemoveItem(int index)
    {
        // var currentNode = node;
        var currentNode = collisionNode[index];
        if(currentNode.next != -1)
        {
            if(currentNode.parent != -1)
            {
                collisionNode[currentNode.parent].next = currentNode.next;
                collisionNode[currentNode.next].parent = currentNode.parent;
            }else
            {
                collisionNode[currentNode.next].parent = -1;
            }
        }else
        {
            if(currentNode.parent != -1)
            {
                collisionNode[currentNode.parent].next = -1;
                gridMap[currentNode.key] = currentNode.parent;
            }else
            {
                gridMap.Remove(currentNode.key);
            }
        }

        collisionNode[index].key = collisionNode[freeNode - 1].key;
        collisionNode[index].parent = collisionNode[freeNode - 1].parent;
        collisionNode[index].next = collisionNode[freeNode - 1].next;
        collisionNode[index].position = collisionNode[freeNode - 1].position;
        collisionNode[index].matrixID = index;
        positionMatrix[index] = positionMatrix[freeNode - 1];

        if(collisionNode[freeNode - 1].next != -1)
        {
            collisionNode[collisionNode[freeNode - 1].next].parent = index;
        }else
        {
            gridMap[collisionNode[freeNode - 1].key] = index;
        }

        if(collisionNode[freeNode - 1].parent != -1)
        {
            collisionNode[collisionNode[freeNode - 1].parent].next = index;
        }

        freeNode -= 1;
        nodeCount -= 1;
    }

    public virtual void DeInit()
    {
        freeNode = 0;
        nodeCount = 0;
        gridMap.Clear();
    }
}