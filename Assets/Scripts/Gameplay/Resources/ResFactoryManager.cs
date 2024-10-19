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

            /// if any error appeared, then make sure to use queue command system
            factory.CheckCollision(hashKey, pos, resCollector);
        }
    }
}