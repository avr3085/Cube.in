using UnityEngine;

/// <summary>
/// Factory SO
/// </summary>
[CreateAssetMenu(menuName ="DataSO/Gameplay/ResFactory", fileName ="ResFactory")]
public class ResFactorySO : ResFactory
{
    [SerializeField] private ResConfig resConfig;
    [SerializeField] private ResType resType;
    [SerializeField] private AnimationCurve moveCurve;
    [SerializeField] private AnimationCurve scaleCurve;
    
    public bool ContainsKey (int haskKey) => hMap.ContainsKey(haskKey);

    protected override ResConfig ResConfig 
    { 
        get => resConfig;
    }

    private const float SqrdColOffset = 0.7f;

    public void DrawMesh()
    {
        if(AnimatorsCount > 0)
        {
            ProcessAnimatorsArray();
        }

        for(int i = 0; i < ResCount; i++)
        {
            positionMatrix[i] = resLookup[i].matrix;
        }
        Graphics.DrawMeshInstanced(resConfig.mesh, 0, resConfig.material, positionMatrix, ResCount);
    }

    /// <summary>
    /// all the items in the array will be animated according to their state
    /// if the items has already been animated then it's state will be switched
    /// </summary>
    private void ProcessAnimatorsArray()
    {
        for(int i = 0; i < AnimatorsCount; i++)
        {
            RNode node = animatorsArray[i];

            if(node.state == RNodeState.Idol)
            {
                node.animatingAlready = false;
                animatorsArray.Remove(node); //removing item from the animators list
            }
            else if(node.state == RNodeState.Removal)
            {
                animatorsArray.Remove(node);
                preGCCounter--;
                base.RemoveRes(node); //permanently removing the item from all the list's
            }
            else if(node.state == RNodeState.SpawnAnimation)
            {
                node.scaleT += Time.deltaTime;
                float val = scaleCurve.Evaluate(node.scaleT);
                node.EvaluateAnimation(val);
            }
            else if(node.state == RNodeState.CollisionAnimation)
            {
                node.moveT += Time.deltaTime;
                float val = moveCurve.Evaluate(node.moveT);
                node.EvaluateAnimation(val);
            }
        }
    }

    /// <summary>
    /// Checks for the collision between given position and the resources lying in the given grid cell hash
    /// </summary>
    /// <param name="hashKey">Hash key for the Uniform grid</param>
    /// <param name="position">Position will be used to check the distance between all the resources staying particular cell</param>
    /// <param name="collector">Caller which collected the resource</pram>
    /// <returns>null</returns>
    public void CheckCollision(int hashKey, Vector3 position, IResCollector collector)
    {
        HNode hNode = hMap[hashKey];
        int mIndex = hNode.index;
        for(int i = 0; i < hNode.totalNodes; i++)
        {
            RNode node = resLookup[mIndex];
            if(node.state == RNodeState.Idol && !node.animatingAlready)
            {
                float distSqrd = (position - node.position).sqrMagnitude;
                if(distSqrd <= SqrdColOffset)
                {
                    node.state = RNodeState.CollisionAnimation;
                    animatorsArray.Add(node);
                    preGCCounter++;

                    collector.OnResCollected(resType);
                }
            }

            mIndex++;
        }
    }
}