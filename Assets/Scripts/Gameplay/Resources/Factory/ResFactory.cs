using System.Collections.Generic;
using UnityEngine;
using Misc;

/// <summary>
/// Resource class, Handles all the resource related methods and properties
/// </summary>
public abstract class ResFactory : ScriptableObject, IRes
{
    protected abstract ResConfig ResConfig{get;}
    protected Matrix4x4[] positionMatrix;
    protected List<RNode> resLookup;
    protected Dictionary<int, HNode> hMap;
    protected List<RNode> animatorsArray;
    protected int ResCount => resLookup.Count; // total resources in the scene, including the non-active ones
    protected int AnimatorsCount => animatorsArray.Count; // total item in the animation list
    protected int activeResCount; // total active resources, excluding the collided resources
    protected int preGCCounter; // keeps track of all the collected resources which is yet to be added to the gcArray

    private List<RNode> gcArray; // contains all the nodes, about to be permanently deleted
    private Queue<RNode> commandQueue; //Command system for systematic node removal
    private bool queueProcessing;
    private bool regenerating;


    public virtual void Init()
    {
        positionMatrix = new Matrix4x4[ResConfig.resCount];
        resLookup = new List<RNode>();
        hMap = new Dictionary<int, HNode>();
        animatorsArray = new List<RNode>();
        gcArray = new List<RNode>();
        activeResCount = 0;
        preGCCounter = 0;

        commandQueue = new Queue<RNode>();
        queueProcessing = false;
        regenerating = false;

        GenerateRes(ResConfig.resCount);
    }

    /// <summary>
    /// Creating new Resource Node for the game at rando position of fixed amount;
    /// </summary>
    /// <param name="count"></param>
    private void GenerateRes(int count)
    {
        for(int i = 0; i < count; i++)
        {
            float fMapSize = ResConfig.mapSize; 
            Vector3 randPos = new Vector3(Random.Range(-fMapSize, fMapSize), 0f, Random.Range(-fMapSize, fMapSize));
            Quaternion randRot = Quaternion.Euler(0f, Random.Range(-180, 180), 0f);

            int hKey = randPos.ToHash();
            RNode newNode = new RNode(hKey, randPos, randRot, true);

            animatorsArray.Add(newNode);
            resLookup.Add(newNode);
            activeResCount++;
        }

        //sort the list by hashID
        resLookup.Sort((x, y) => x.key.CompareTo(y.key));

        InitHashMap();
    }

    /// <summary>
    /// Initializing the hash map for the Resource Lookup Table
    /// </summary>
    private void InitHashMap()
    {
        int i = 0;
        int currentKey = -1;
        foreach(RNode n in resLookup)
        {
            if(n.key != currentKey)
            {
                currentKey = n.key;
                HNode hNode = new HNode(i);
                hMap.Add(currentKey, hNode);
            }else
            {
                hMap[currentKey].totalNodes++;
            }

            i++;
        }

        if(regenerating)
        {
            regenerating = false;
        }
    }

    /// <summary>
    /// Cleaning up all the unused resource from the garbage collector
    /// and generating the new resources
    /// </summary>
    private void ClearGC()
    {
        int totalRemovedRes = 0;

        for(int i = gcArray.Count - 1; i >= 0; i--)
        {
            resLookup.Remove(gcArray[i]);
            totalRemovedRes++;
        }

        gcArray.Clear();
        hMap.Clear();

        GenerateRes(totalRemovedRes);
    }

    /// <summary>
    /// Animating the collided resources and later on adding it to the Garbage collector
    /// </summary>
    /// <param name="hashKey"></param>
    /// <param name="node"></param>
    public virtual void RemoveRes(RNode node)
    {
        commandQueue.Enqueue(node);

        if(!queueProcessing)
        {
            queueProcessing = true;
            DoNext();
        }
    }

    private void DoNext()
    {
        if(commandQueue.Count == 0)
        {
            queueProcessing = false;
            return;
        }

        RNode node = commandQueue.Dequeue();
        gcArray.Add(node);
        activeResCount--;

        if(preGCCounter == 0 && activeResCount <= ResConfig.reGenThres && ResConfig.autoGenerate)
        {
            regenerating = true;
            ClearGC();
        }

        DoNext();
    }

    /// <summary>
    /// Cleaning up
    /// </summary>
    public virtual void DeInit()
    {
        resLookup.Clear();
        animatorsArray.Clear();
        gcArray.Clear();
        hMap.Clear();
    }
}