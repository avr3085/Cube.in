using System.Collections.Generic;
using UnityEngine;
using Misc;

/// <summary>
/// Resource class, Handles all the resource related methods and properties
/// </summary>
public abstract class ResFactory : ScriptableObject, IRes
{
    private int mapSize;
    private int resCount;

    protected abstract ResConfig ResConfig{get;}
    protected Matrix4x4[] positionMatrix;
    protected List<RNode> resLookup;
    protected Dictionary<int, HNode> hMap;
    protected int ResCount => resLookup.Count;

    //GC List -> contains all the hash cell, which should be deleted later
    private readonly List<int> gcArray = new List<int>();
    protected int activeResCount;

    public virtual void Init()
    {
        this.mapSize = ResConfig.mapSize;
        this.resCount = ResConfig.resCount;

        positionMatrix = new Matrix4x4[resCount];
        resLookup = new List<RNode>();
        hMap = new Dictionary<int, HNode>();
        activeResCount = 0;

        GenerateRes(resCount);
    }

    /// <summary>
    /// Creating new Resource Node for the game at rando position of fixed amount;
    /// </summary>
    /// <param name="count"></param>
    private void GenerateRes(int count)
    {
        for(int i = 0; i < count; i++)
        {
            float fMapSize = mapSize; 
            Vector3 randPos = new Vector3(Random.Range(-fMapSize, fMapSize), 0f, Random.Range(-fMapSize, fMapSize));
            Quaternion randRot = Quaternion.Euler(0f, Random.Range(-180, 180), 0f);

            int hashKey = randPos.ToHash();
            RNode newNode = new RNode(hashKey, randPos, randRot);
            resLookup.Add(newNode);
            activeResCount += 1;
        }

        //sort the list by hashID
        resLookup.Sort((x, y) => x.hashKey.CompareTo(y.hashKey));

        //creating indices map
        InitHashMap();
    }

    /// <summary>
    /// Initializing the hash map for the Resource Lookup Table
    /// </summary>
    private void InitHashMap()
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

    /// <summary>
    /// Re Generate the removed resources
    /// </summary>
    /// <param name="amount"></param>
    public virtual void AddRes(int amount)
    {
        if(gcArray.Count != 0)
        {
            ClearGC();
        }

        GenerateRes(amount);
    }

    /// <summary>
    /// Cleaning up all the unused resource from the garbage collector
    /// and generating the new resources
    /// </summary>
    private void ClearGC()
    {
        gcArray.Sort();
        for(int i = gcArray.Count - 1; i >= 0; i--)
        {
            //remove the item from list
            int hKey = gcArray[i];
            HNode n = hMap[hKey];
            for(int j = 0; j < n.totalNode; j++)
            {
                resLookup.RemoveAt(n.startIndex);
            }

            // hMap.Remove(hKey); //Since we will clear the map after it, no need to use this method
        }

        gcArray.Clear();
    }

    /// <summary>
    /// Animating the collided resources and later on adding it to the Garbage collector
    /// </summary>
    /// <param name="hashKey"></param>
    /// <param name="node"></param>
    public virtual void RemoveRes(int hashKey, RNode node)
    {
        node.animate = true;
        hMap[hashKey].nodeCount -= 1;
        activeResCount -= 1;
        
        if(hMap[hashKey].nodeCount <= 0)
        {
            gcArray.Add(hashKey);
        }
    }

    /// <summary>
    /// Cleaning up
    /// </summary>
    public virtual void DeInit()
    {
        resLookup.Clear();
        gcArray.Clear();
        hMap.Clear();
    }
}