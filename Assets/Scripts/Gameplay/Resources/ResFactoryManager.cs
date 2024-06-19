using System.Threading.Tasks;
using Unity.VisualScripting;
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

    // public void ChekcCol(Vector2 position)
    // {
    //     foreach(var factory in resFactories)
    //     {
    //         var collisionItem = factory.GetGridItems(position.GetHashCode());

    //         foreach(var col in collisionItem)
    //         {
    //             if(col ==  null)
    //             {
    //                 Debug.Log("col is null");
    //             }
    //             else
    //             {
    //                 Debug.Log("1");
    //             }
    //             // if()
    //             // if(Vector2.Distance(position, col.position) < 0.5f)
    //             // {
    //             //     Debug.Log(col.key);
    //             //     // factory.RemoveItem(col.)
    //             //     //remove the item and start the callback
    //             // }
    //         }
    //     }
        
    // }


    public void DrawGizmo()
    {
        foreach(var item in resFactories)
        {
            var itemMap = item.GridMap;

            foreach(var val in itemMap.Keys)
            {
                var mapVal = itemMap[val];

                var node = item.CollisionNode[mapVal];
                Gizmos.DrawSphere(node.position, 0.3f);
            }
        }
    
    }
    
}