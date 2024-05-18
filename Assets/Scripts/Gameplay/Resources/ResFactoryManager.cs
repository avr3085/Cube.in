using UnityEngine;

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

    public void Initialize()
    {
        foreach(var factory in resFactories)
        {
            factory.Init();
        }
    }

    public void Draw()
    {
        foreach(var factory in resFactories)
        {
            factory.DrawMesh();
        }
    }

    public void DeInitialize()
    {
        foreach(var factory in resFactories)
        {
            factory.DeInit();
        }
    }
    
}