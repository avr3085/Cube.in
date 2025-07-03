using UnityEngine;

[CreateAssetMenu(fileName = "BotStats", menuName = "AI/Data/Stats")]
public class BotStats : ScriptableObject
{
    [Range(1, 10)] public int moveSpeed = 1;
    [Range(1, 10)] public int rotationSpeed = 2;
    [Range(1, 10)] public int visionRange = 4;
    public float turnFactor = 5f;
}