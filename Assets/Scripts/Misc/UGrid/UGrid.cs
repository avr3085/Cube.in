using UnityEngine;
using System.Collections.Generic;
using MiscUtils;

public class UGrid
{
    private readonly int EXTRA_SPACE = 8;
    public int entityCount;
    public int mapSize;

    private Matrix4x4[] positionMatrix;
    private GNode[] gridNode;
    private Dictionary<int, int> gridMap;


    public Matrix4x4[] PoisitonMatrix => positionMatrix;
    public GNode[] GridNode => gridNode;
    public Dictionary<int, int> GridMap => gridMap;
    public int NodeCount => nodeCount;


    private int freeNode = 0;
    private int nodeCount = 0;

    public UGrid(int entityCount, int mapSize)
    {
        this.entityCount = entityCount + EXTRA_SPACE;
        this.mapSize = mapSize;
        positionMatrix = new Matrix4x4[entityCount];
        gridNode = new GNode[entityCount];
        gridMap = new Dictionary<int, int>();

        GenerateRes(entityCount);
    }

    public void GenerateRes(int count)
    {
        for(int i = 0; i < count; i++)
        {
            AddItem(mapSize);
        }
    }

    private void AddItem(int map)
    {
        float floatMapSize = map; 
        var randPos = new Vector2(Random.Range(-floatMapSize, floatMapSize), Random.Range(-floatMapSize, floatMapSize));
        Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(-180, 180));
        positionMatrix[freeNode] = Matrix4x4.TRS(randPos, rotation, Vector3.one);

        int hashKey = randPos.GetHashKey(map*2);
        gridNode[freeNode] = new GNode(hashKey, randPos, freeNode);
        if(!gridMap.ContainsKey(hashKey))
        {
            gridMap.Add(hashKey, freeNode);
        }else
        {
            var indices = gridMap[hashKey];
            gridNode[indices].next = freeNode;
            gridNode[freeNode].parent = indices;
            gridMap[hashKey] = freeNode;
        }

        freeNode += 1;
        nodeCount += 1;
    }

    private void RemoveItem(int index)
    {
        var currentNode = gridNode[index];
        if(currentNode.next != -1)
        {
            if(currentNode.parent != -1)
            {
                gridNode[currentNode.parent].next = currentNode.next;
                gridNode[currentNode.next].parent = currentNode.parent;
            }else
            {
                gridNode[currentNode.next].parent = -1;
            }
        }else
        {
            if(currentNode.parent != -1)
            {
                gridNode[currentNode.parent].next = -1;
                gridMap[currentNode.key] = currentNode.parent;
            }else
            {
                gridMap.Remove(currentNode.key);
            }
        }

        gridNode[index].key = gridNode[freeNode - 1].key;
        gridNode[index].parent = gridNode[freeNode - 1].parent;
        gridNode[index].next = gridNode[freeNode - 1].next;
        gridNode[index].position = gridNode[freeNode - 1].position;
        gridNode[index].matrixID = index;
        positionMatrix[index] = positionMatrix[freeNode - 1];

        if(gridNode[freeNode - 1].next != -1)
        {
            gridNode[gridNode[freeNode - 1].next].parent = index;
        }else
        {
            gridMap[gridNode[freeNode - 1].key] = index;
        }

        if(gridNode[freeNode - 1].parent != -1)
        {
            gridNode[gridNode[freeNode - 1].parent].next = index;
        }

        freeNode -= 1;
        nodeCount -= 1;
    }

    public void DeInit()
    {
        gridMap.Clear();
    }
}