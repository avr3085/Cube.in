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
}