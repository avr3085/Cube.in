using UnityEngine;

[CreateAssetMenu(menuName ="DataSO/Gameplay/ResFactory", fileName ="ResFactory")]
public class ResFactorySO : ResFactory
{
    [SerializeField] private ResConfig resConfig;
    [SerializeField] private ResType resType;
    [SerializeField] private AnimationCurve moveYCurve;

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
            // if(resLookup[i].animate)
            // {
            //     resLookup[i].t += Time.deltaTime;
            //     var time = resLookup[i].t;
            //     var val = moveYCurve.Evaluate(time);
            //     resLookup[i].MoveUp(val);
            // }
            positionMatrix[i] = resLookup[i].matrix;
        }

        // for(int i = 0; i< NodeCount; i++)
        // {
        //     if(resLookup[i].remove)
        //     {
        //         var currenNode = collisionNode[i];
        //         collisionNode.Remove(currenNode);
        //         nodeCount -= 1;
        //     }
        // }

        // if(useMultiColor)
        // {
        //     Graphics.DrawMeshInstanced(resConfig.mesh, 0, resConfig.material, positionMatrix, nodeCount, block);
        //     return;
        // }

        Graphics.DrawMeshInstanced(resConfig.mesh, 0, resConfig.material, positionMatrix, NodeCount);
    }

}