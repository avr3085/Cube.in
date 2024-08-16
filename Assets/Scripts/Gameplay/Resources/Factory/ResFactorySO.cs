using UnityEngine;

[CreateAssetMenu(menuName ="DataSO/Gameplay/ResFactory", fileName ="ResFactory")]
public class ResFactorySO : ResFactory
{
    [SerializeField] private ResConfig resConfig;
    [SerializeField] private ResType resType;
    [SerializeField] private AnimationCurve moveYCurve;

    [Header("Multi Color"), SerializeField] private bool useMultiColor;

    protected override ResConfig ResConfig 
    { 
        get => resConfig;
    }

    protected override bool IsMultiColor => useMultiColor;
    public bool ContainsKey (int haskKey) => map.ContainsKey(haskKey);
    public ResType ResourceType => resType;

    public void DrawMesh()
    {
        for(int i = 0; i < nodeCount; i++)
        {
            if(collisionNode[i].shouldMove)
            {
                collisionNode[i].t += Time.deltaTime;
                var time = collisionNode[i].t;
                var val = moveYCurve.Evaluate(time);
                collisionNode[i].MoveUp(val);
            }
            positionMatrix[i] = collisionNode[i].matrix;
        }

        for(int i = 0; i< nodeCount; i++)
        {
            if(collisionNode[i].remove)
            {
                var currenNode = collisionNode[i];
                collisionNode.Remove(currenNode);
                nodeCount -= 1;
            }
        }

        if(useMultiColor)
        {
            Graphics.DrawMeshInstanced(resConfig.mesh, 0, resConfig.material, positionMatrix, nodeCount, block);
            return;
        }

        Graphics.DrawMeshInstanced(resConfig.mesh, 0, resConfig.material, positionMatrix, nodeCount);
    }

}