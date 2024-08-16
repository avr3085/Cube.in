using UnityEngine;
using System.Collections.Generic;
using System;

public class ResFactoryManager : MonoBehaviour
{
    [SerializeField] private ResFactorySO[] resFactories = default;

    public static ResFactoryManager Instance{get; private set;}
    private Queue<Tuple<int, ICollect>> requestQueue = new Queue<Tuple<int, ICollect>>();
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

    private void Request(int hashKey, ICollect i)
    {
        foreach(var factory in resFactories)
        {
            if (factory.ContainsKey(hashKey))
            {
                requestQueue.Enqueue(
                    new Tuple<int, ICollect>(hashKey, i)
                );

                //dequeue here

                if(!isProcessing)
                {
                    isProcessing = true;
                    DoNext(i);
                }
            }
        }
    }

    private void DoNext(ICollect i)
    {
        if(requestQueue.Count == 0)
        {
            isProcessing = false;
            return;
        }
        //removing item;
        Tuple<int, ICollect> request = requestQueue.Dequeue();

        foreach(var factory in resFactories)
        {
            if(factory.ContainsKey(request.Item1))
            {
                factory.RemoveItem(request.Item1);
                request.Item2.OnCollect(factory.ResourceType);
            }
        }
        DoNext(i);
    }

    public void RequestCollect(int hashKey, ICollect i)
    {
        Request(hashKey, i);
    }

    public bool ContainsKey(int hashKey)
    {
        foreach(var factory in resFactories)
        {
            if(factory.ContainsKey(hashKey))
            {
                return true;
            }
        }

        return false;
    }
}