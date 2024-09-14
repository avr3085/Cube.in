using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Factory SO
/// </summary>
[CreateAssetMenu(menuName ="DataSO/Gameplay/ResFactory", fileName ="ResFactory")]
public class ResFactorySO : ResFactory
{
    [SerializeField] private ResConfig resConfig;
    [SerializeField] private ResType resType;
    [SerializeField] private AnimationCurve animY;

    private const float colOffset = 0.7f;

    protected override ResConfig ResConfig 
    { 
        get => resConfig;
    }

    public bool ContainsKey (int haskKey) => hMap.ContainsKey(haskKey);
    public ResType ResourceType => resType;

    public void DrawMesh()
    {
        for(int i = 0; i < activeResCount; i++)
        {
            if(resLookup[i].animate)
            {
                resLookup[i].t += Time.deltaTime;
                var time = resLookup[i].t;
                var posVal = animY.Evaluate(time);
                resLookup[i].MoveUp(posVal);
            }

            positionMatrix[i] = resLookup[i].matrix;
        }

        Graphics.DrawMeshInstanced(resConfig.mesh, 0, resConfig.material, positionMatrix, ResCount);
    }

    /// <summary>
    /// Checks for the collision between given position and the resources lying in the given grid cell hash
    /// </summary>
    /// <param name="hashKey">Hash key for the Uniform grid</param>
    /// <param name="position">Position will be used to check the distance between all the resources staying particular cell</param>
    /// <returns>List of res type which is getting collided</returns>
    public IEnumerable<ResType> HashCollided(int hashKey, Vector3 position)
    {
        HNode hNode = hMap[hashKey];
        int index = hNode.startIndex;
        for(int i = 0; i < hNode.totalNode; i++)
        {
            RNode rNode = resLookup[index];
            if(!rNode.animate && !rNode.hasAnimated)
            {
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

    /// <summary>
    /// Re generating the resouces, when reGenThres has been reached
    /// </summary>
    /// <param name="hashKey"></param>
    /// <param name="node"></param>
    public override void RemoveRes(int hashKey, RNode node)
    {
        base.RemoveRes(hashKey, node);
        
        if(activeResCount < resConfig.reGenThres && resConfig.autoGenerate)
        {
            int reGenAmount = resConfig.resCount - activeResCount;
            base.AddRes(reGenAmount);
        }

    }
}