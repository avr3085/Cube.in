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
    protected int activeResCount;
    protected List<RNode> animationList;
    protected int AnimationListCount => animationList.Count;

    //GC List -> contains all the hash cell, which should be deleted later
    protected List<int> gcArray;

    public virtual void Init()
    {
        this.mapSize = ResConfig.mapSize;
        this.resCount = ResConfig.resCount;

        positionMatrix = new Matrix4x4[resCount];
        resLookup = new List<RNode>();
        hMap = new Dictionary<int, HNode>();
        // animationList = new List<int>();
        animationList = new List<RNode>();
        gcArray = new List<int>();
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

            int hKey = randPos.ToHash();
            RNode newNode = new RNode(hKey, randPos, randRot);

            animationList.Add(newNode);

            resLookup.Add(newNode);
            activeResCount++;
        }

        //sort the list by hashID
        resLookup.Sort((x, y) => x.key.CompareTo(y.key));

        //creating indices map
        InitHashMap();
    }

    /// <summary>
    /// Initializing the hash map for the Resource Lookup Table
    /// </summary>
    private void InitHashMap()
    {
        // hMap.Clear(); //cleaning map, already cleared
        int i = 0;
        foreach(RNode n in resLookup)
        {
            if(!hMap.ContainsKey(n.key))
            {
                HNode hNode  = new HNode(i);
                hMap.Add(n.key, hNode);
            }else
            {
                hMap[n.key].activeNodes++;
                hMap[n.key].totalNodes++;
            }

            i++;
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
            ClearGC(amount);
            Debug.Log("cach clear");
        }

        // GenerateRes(amount);
    }

    /// <summary>
    /// Cleaning up all the unused resource from the garbage collector
    /// and generating the new resources
    /// </summary>
    private void ClearGC(int amount)
    {
        gcArray.Sort();
        for(int i = gcArray.Count - 1; i >= 0; i--)
        {
            //remove the item from list
            int hKey = gcArray[i];
            HNode n = hMap[hKey];
            for(int j = 0; j < n.totalNodes; j++)
            {
                resLookup.RemoveAt(n.startIndex);
            }

            // hMap.Remove(hKey); //Since we will clear the map after it, no need to use this method
        }

        hMap.Clear();
        gcArray.Clear();

        GenerateRes(amount);
    }

    /// <summary>
    /// Animating the collided resources and later on adding it to the Garbage collector
    /// </summary>
    /// <param name="hashKey"></param>
    /// <param name="node"></param>
    // public virtual void RemoveRes(int hashKey, RNode node)
    public virtual void RemoveRes(RNode node)
    {
        int key = node.key;
        hMap[key].activeNodes--;
        
        activeResCount--;
        if(hMap[key].activeNodes <= 0)
        {
            gcArray.Add(key);
        }

        if(activeResCount == ResConfig.reGenThres && ResConfig.autoGenerate)
        {
            int reGenAmount = ResConfig.resCount - activeResCount;
            AddRes(reGenAmount);
        }
    }


    /// <summary>
    /// Cleaning up
    /// </summary>
    public virtual void DeInit()
    {
        resLookup.Clear();
        animationList.Clear();
        gcArray.Clear();
        hMap.Clear();
    }
}