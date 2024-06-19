using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="DataSO/Gameplay/ResFactory", fileName ="ResFactory")]
public class ResFactorySO : ResFactory
{
    [SerializeField] private ResConfig resConfig;
    [SerializeField] private ResType resType;

    // public CNode GetCNode(int index)
    // {
    //     return collisionNode[index];
    // }

    protected override ResConfig ResConfig 
    { 
        get => resConfig;
    }

    public void DrawMesh()
    {
        Graphics.DrawMeshInstanced(resConfig.mesh, 0, resConfig.material, positionMatrix, NodeCount);
    }

    public IEnumerable<CNode> GetGridItems(int index)
    {
        if(gridMap.ContainsKey(index))
        {
            var item = gridMap[index];
            while(item != -1)
            {
                yield return collisionNode[item];
                item = collisionNode[item].next;
            }
            
        }else
        {
            yield return null;
        }
    }
}