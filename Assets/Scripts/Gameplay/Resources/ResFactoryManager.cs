using UnityEngine;

/// <summary>
/// This class is like a bridge between Resource collector and Resource factories.
/// </summary>
public class ResFactoryManager : MonoBehaviour
{
    [SerializeField] private ResFactorySO[] resFactories = default;

    public static ResFactoryManager Instance{get; private set;}

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

    private void LateUpdate()
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
    /// Bridge between Caller and reciever
    /// Remove resource if caller collides with it
    /// </summary>
    /// <param name="hashKey"></param>
    /// <param name="pos"></param>
    /// <param name="resCollector">Caller</param>
    public void CheckCollision(int hashKey, Vector3 pos, IResCollector resCollector)
    {
        foreach(var factory in resFactories)
        {
            if(factory.ContainsKey(hashKey))
            {
                factory.CheckCollision(hashKey, pos, resCollector);
            }
        }
    }

    /// <summary>
    /// Calculates the average position of resources in the given hash for all the factories
    /// </summary>
    /// <param name="hashKey"></param>
    /// <returns></returns>
    public Vector3 GetAveragePosition(int hashKey)
    {
        Vector3 avgPos = Vector3.zero;
        int count = 0;
        foreach(var factory in resFactories)
        {
            if(!factory.ContainsKey(hashKey))
            {
                continue;
            }

            avgPos += factory.AveragePosition(hashKey);
            count++;
        }

        if(count > 1)
        {
            avgPos = avgPos/count;
        }

        return avgPos;
    }
}