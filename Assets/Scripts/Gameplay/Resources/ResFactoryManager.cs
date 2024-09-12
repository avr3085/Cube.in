using UnityEngine;
using System.Collections.Generic;
using System;

public class ResFactoryManager : MonoBehaviour
{
    [SerializeField] private ResFactorySO[] resFactories = default;

    public static ResFactoryManager Instance{get; private set;}
    private Queue<Tuple<ResType, ICollect>> requestQueue = new Queue<Tuple<ResType, ICollect>>();
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

    public void CheckCollision(int hashKey, Vector3 pos, ICollect onCollect)
    {
        foreach(var factory in resFactories)
        {
            if(!factory.ContainsKey(hashKey))
            {
                continue;
            }
            var colResArray = factory.hashCollided(hashKey, pos);
            foreach(var res in colResArray)
            {
                requestQueue.Enqueue(
                    new Tuple<ResType, ICollect>(res, onCollect)
                );
            }
            
        }

        if(!isProcessing)
        {
            isProcessing = true;
            DoNext();
        }

    }

    private void DoNext()
    {
        if(requestQueue.Count == 0)
        {
            isProcessing = false;
            return;
        }

        Tuple<ResType, ICollect> request = requestQueue.Dequeue();
        request.Item2.OnCollect(request.Item1);

        DoNext();
    }

}