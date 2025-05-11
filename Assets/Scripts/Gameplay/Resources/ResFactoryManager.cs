using UnityEngine;
using Misc;

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
    /// Returns the best Nerest Resorce Box position from the player position
    /// </summary>
    /// <param name="hashKey">Hash key for the uniform grid</param>
    /// <param name="pos">Current Player position</param>
    /// <returns>Best Nearest Resource position</returns>
    public Vector3 GetNearest(int hashKey, Vector3 pos)
    {
        /*
        If you want to check the distance of all the nearest resource type,
        Such as Edible Crate, Death crate, Mystery box. Then use the below code
        */
        // Vector3 nearestPos = Vector3.zero;
        // float maxSqrdDistaceCheck = HelperUtils.MaxSqrdDistaceCheck;

        // foreach(var factory in resFactories)
        // {
        //     if(factory.ContainsKey(hashKey))
        //     {
        //         Vector3 currentNearest = factory.CheckNearest(hashKey, pos);
        //         float distSqrd = (currentNearest - pos).sqrMagnitude;
        //         if(distSqrd < maxSqrdDistaceCheck)
        //         {
        //             maxSqrdDistaceCheck = distSqrd;
        //             nearestPos = currentNearest;
        //         }
        //     }
        // }

        // return nearestPos;

        /*
        Only Checking for the edible crate
        The above code is checking all types of resources
        */

        if(!resFactories[0].ContainsKey(hashKey))
        {
            return pos;
        }
        
        return resFactories[0].CheckNearest(hashKey, pos);
    }

    // / <summary>
    // / Calculates the average position of resources in the given hash for all the factories
    // / </summary>
    // / <param name="hashKey"></param>
    // / <returns></returns>
    // public Vector3 GetAveragePosition(int hashKey)
    // {
        // Vector3 avgPos = Vector3.zero;
        // int count = 0;
        // foreach(var factory in resFactories)
        // {
        //     if(!factory.ContainsKey(hashKey))
        //     {
        //         continue;
        //     }

        //     avgPos += factory.AveragePosition(hashKey);
        //     count++;
        // }

        // if(count > 1)
        // {
        //     avgPos = avgPos/count;
        // }

        // return avgPos;
    // }
}