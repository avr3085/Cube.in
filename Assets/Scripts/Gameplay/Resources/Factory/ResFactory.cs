using System.Collections.Generic;
using UnityEngine;
using MiscUtils;

public abstract class ResFactory : ScriptableObject, IRes
{
    private int mapSize;
    private int resCount;
    private int extraSpace;

    protected abstract ResConfig ResConfig{get;}
    protected Matrix4x4[] positionMatrix;
    protected List<RNode> resLookup;
    protected Dictionary<int, HNode> hMap;
    protected int NodeCount => resLookup.Count;

    //GC Queue
    private readonly Queue<int> gcQueue = new Queue<int>();
    private bool isProcessing = false;

    public virtual void Init()
    {
        this.mapSize = ResConfig.mapSize;
        this.extraSpace = ResConfig.extraSpace;
        this.resCount = ResConfig.resCount + extraSpace;

        positionMatrix = new Matrix4x4[resCount];
        resLookup = new List<RNode>();
        hMap = new Dictionary<int, HNode>();

        GenerateRes(resCount);
    }

    private void GenerateRes(int count)
    {

        for(int i = 0; i < count; i++)
        {
            float fMapSize = mapSize; 
            Vector3 randPos = new Vector3(Random.Range(-fMapSize, fMapSize), 0f, Random.Range(-fMapSize, fMapSize));
            Quaternion randRot = Quaternion.Euler(0f, Random.Range(-180, 180), 0f);

            int hashKey = randPos.GetHash();
            RNode newNode = new RNode(hashKey, randPos, randRot);
            resLookup.Add(newNode);
        }

        //sort the list by hashID
        resLookup.Sort((x, y) => x.hashKey.CompareTo(y.hashKey));

        //creating indices map
        InitHashMap(count);
    }

    private void InitHashMap(int count)
    {
        hMap.Clear(); //cleaning map
        
        int i = 0;
        foreach(RNode node in resLookup)
        {
            if(!hMap.ContainsKey(node.hashKey))
            {
                HNode hashNode  = new HNode(i);
                hMap.Add(node.hashKey, hashNode);
            }else
            {
                hMap[node.hashKey].nodeCount += 1;
                hMap[node.hashKey].totalNode += 1;
            }
            
            i += 1;
        }
    }

    public virtual void AddItem(int amount)
    {
        // Handling GC queue before creating new resources
        if(gcQueue.Count > 0)
        {
            ClearGCQ();
        }

        GenerateRes(amount);
    }

    private void ClearGCQ()
    {
        //Try using sorted priority queue for this problem
    }

    public virtual void RemoveItem(int hashKey, RNode node)
    {
        node.animate = true;
        hMap[hashKey].nodeCount -= 1;
        
        if(hMap[hashKey].nodeCount <= 0)
        {
            gcQueue.Enqueue(hashKey);
        }
    }

    public virtual void DeInit()
    {
        hMap.Clear();
    }
}