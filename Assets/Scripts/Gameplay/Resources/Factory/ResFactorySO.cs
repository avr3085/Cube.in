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

    private const float SqrdColOffset = 0.7f;

    protected override ResConfig ResConfig 
    { 
        get => resConfig;
    }

    public bool ContainsKey (int haskKey) => hMap.ContainsKey(haskKey);

    public void DrawMesh()
    {
        if(animationList.Count > 0)
        {
            ProcessAnimationList();
        }

        for(int i = 0; i < ResCount; i++)
        {
            positionMatrix[i] = resLookup[i].matrix;
        }

        Graphics.DrawMeshInstanced(resConfig.mesh, 0, resConfig.material, positionMatrix, ResCount);
    }

    /// <summary>
    /// This function process the animation list
    /// all the items in the list will be animated according to their state
    /// if the items has already been animated then it's state will be switched
    /// </summary>
    private void ProcessAnimationList()
    {
        for(int i = 0; i < AnimationListCount; i++)
        {
            RNode node = animationList[i];

            if(node.state == RNodeState.Idol)
            {
                animationList.Remove(node); //removing item from the animation list
            }
            else if(node.state == RNodeState.Removal)
            {
                animationList.Remove(node);
                base.RemoveRes(node); //permanently removing the item from all the list's
            }
            else if(node.state == RNodeState.SpawnAnimation)
            {
                node.scaleT += Time.deltaTime;
                float val = scaleCurve.Evaluate(node.scaleT);
                node.EvaluateAnimation(val);
            }
            else if (node.state == RNodeState.CollisionAnimation)
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
    /// <returns>List of res type which is getting collided</returns>
    // public IEnumerable<ResType> HashCollided(int hashKey, Vector3 position)
    public void CheckCollision(int hashKey, Vector3 position, IResCollector collector)
    {
        // make a queue here which will process the request
        HNode hNode = hMap[hashKey];
        int index = hNode.startIndex;
        for(int i = 0; i < hNode.totalNodes; i++)
        {
            RNode rNode = resLookup[index];
            if(rNode.state == RNodeState.Idol)
            {
                float distSqrd = (position - rNode.position).sqrMagnitude;
                if(distSqrd <= SqrdColOffset)
                {
                    rNode.state = RNodeState.CollisionAnimation;
                    animationList.Add(rNode);

                    collector.OnResCollected(resType);
                }
            }
    
            index++;
        }
    }
}