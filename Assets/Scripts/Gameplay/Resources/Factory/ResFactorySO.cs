using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName ="DataSO/Gameplay/ResFactory", fileName ="ResFactory")]
public class ResFactorySO : ResFactory
{
    [SerializeField] private ResConfig resConfig;
    [SerializeField] private ResType resType;
    [SerializeField] private AnimationCurve moveYCurve;

    private const float colOffset = 0.5f;

    protected override ResConfig ResConfig 
    { 
        get => resConfig;
    }

    public bool ContainsKey (int haskKey) => hMap.ContainsKey(haskKey);
    public ResType ResourceType => resType;

    public void DrawMesh()
    {
        for(int i = 0; i < NodeCount; i++)
        {
            if(resLookup[i].animate)
            {
                resLookup[i].t += Time.deltaTime;
                var time = resLookup[i].t;
                var val = moveYCurve.Evaluate(time);
                resLookup[i].MoveUp(val);
            }

            positionMatrix[i] = resLookup[i].matrix;
        }

        Graphics.DrawMeshInstanced(resConfig.mesh, 0, resConfig.material, positionMatrix, NodeCount);
    }

    /// <summary>
    /// Checks for the collision using the hashKey in the grid cell
    /// </summary>
    /// <param name="hashKey">Hash key for the Uniform grid</param>
    /// <param name="position">Position will be used to check the distance between all the resources staying particular cell</param>
    public IEnumerable<ResType> hashCollided(int hashKey, Vector3 position)
    {
        HNode hNode = hMap[hashKey];
        int index = hNode.startIndex;
        for(int i = 0; i < hNode.totalNode; i++)
        {
            RNode rNode = resLookup[index];
            float colDist = (position - rNode.position).sqrMagnitude;

            if(colDist < colOffset)
            {
                RemoveRes(hashKey, rNode);
                index += 1;
                yield return resType;
            }
        }
    }
}