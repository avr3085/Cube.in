using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// This class is like a bridge between Resource collector and Resource factories.
/// </summary>
public class ResFactoryManager : MonoBehaviour
{
    [SerializeField] private ResFactorySO[] resFactories = default;

    public static ResFactoryManager Instance{get; private set;}

    /// <summary>
    /// ResCollector request queue
    /// Raises OnResCollected method
    /// </summary>
    private Queue<Tuple<ResType, IResCollector>> requestQueue = new Queue<Tuple<ResType, IResCollector>>();
    private bool isProcessing = false;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
        }else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        foreach(var factory in resFactories)
        {
            factory.Init();
        }
    }

    private void Update()
    {
        foreach(var factory in resFactories)
        {
            factory.DrawMesh();
        }
    }

    private void OnDisable()
    {
        foreach(var factory in resFactories)
        {
            factory.DeInit();
        }
    }

    /// <summary>
    /// Checks collision between given position and nearby resources
    /// If any resource is getting collided with the input position
    /// then that resource type is added to the requst queue for further processing
    /// </summary>
    /// <param name="hashKey"></param>
    /// <param name="pos"></param>
    /// <param name="resCollector">Caller</param>
    public void CheckCollision(int hashKey, Vector3 pos, IResCollector resCollector)
    {
        // this function is called every frame
        foreach(var factory in resFactories)
        {
            if(!factory.ContainsKey(hashKey))
            {
                continue;
            }

            var colResArray = factory.HashCollided(hashKey, pos);
            foreach(var res in colResArray)
            {
                requestQueue.Enqueue(
                    new Tuple<ResType, IResCollector>(res, resCollector)
                );
            }

            if(!isProcessing)
            {
                isProcessing = true;
                DoNext();
            }
        }
    }

    /// <summary>
    /// Calculates the average position of all the factories
    /// </summary>
    /// <param name="hashKey"></param>
    /// <returns>return average position</returns>
    public Vector3 GetAveragePosition(int hashKey)
    {
        Vector3 avgPos = Vector3.zero;
        int validFactory = 0;
        foreach(var factory in resFactories)
        {
            if(!factory.ContainsKey(hashKey))
            {
                continue;
            }

            avgPos += factory.CalculateAveragePosition(hashKey);
            validFactory += 1;
        }

        return avgPos / validFactory;
        // return avgPos;
    }

    /// <summary>
    /// Request queue is being processed. and the callback method is being invoked
    /// </summary>
    private void DoNext()
    {
        if(requestQueue.Count == 0)
        {
            isProcessing = false;
            return;
        }

        Tuple<ResType, IResCollector> request = requestQueue.Dequeue();
        request.Item2.OnResCollected(request.Item1);
        DoNext();
    }

}