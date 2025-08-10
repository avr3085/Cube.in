using UnityEngine;

[CreateAssetMenu(fileName = "BotStats", menuName = "AI/Data/Stats")]
public class BotStats : ScriptableObject
{
    [Range(1, 10)] public int moveSpeed = 1;
    [Range(1, 10)] public int rotationSpeed = 2;
    [Range(1, 10)] public int visionRange = 4;
    [Range(1, 50)] public int chaseTargetDistance = 8;
    public float turnFactor = 5f;

    public int ChaseTargeDistSqrd => chaseTargetDistance * chaseTargetDistance;
    public int VisionRangeSqrd => visionRange * visionRange;

    [Header("Overlap Test")]
    [Range(1, 10)] public int halfRadius = 1;
    public LayerMask lMask;
}