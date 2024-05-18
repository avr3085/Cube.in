using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="DataSO/Gameplay/ResFactory", fileName ="ResFactory")]
public class ResFactorySO : ResFactory
{
    [SerializeField] private ResConfig resConfig;

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
        if(!gridMap.ContainsKey(index))
        {
            yield return null;
        }else
        {
            var item = gridMap[index];
            while(item != -1)
            {
                yield return collisionNode[item];
                item = collisionNode[item].next;
            }
        }
    }
}