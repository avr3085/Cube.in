using UnityEngine;

/// <summary>
/// Configuration for the resources, which will be generated
/// [Note - mapSize must be smaller than the map size in Utils class, otherwise the programme will show bugs]
/// </summary>

[CreateAssetMenu(menuName ="DataSO/Gameplay/ResConfig", fileName ="ResConfig")]
public class ResConfig : ScriptableObject
{
    public int mapSize; // size of the floor
    public int resCount; // number of resources to be generated
    public int reGenThres; // regeneration will take place after the total active resCount is less than the reGenThres
    public Mesh mesh;
    public Material material;
    public bool autoGenerate; // Auto Regeneration of item will take place only when this option is set
}