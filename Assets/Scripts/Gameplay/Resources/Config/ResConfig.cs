using UnityEngine;

/// <summary>
/// Configuration for the resources, which will be generated
/// [Note - mapSize must be smaller than the map size in Utils class, otherwise the programme will show bugs]
/// </summary>

[CreateAssetMenu(menuName ="DataSO/Gameplay/ResConfig", fileName ="ResConfig")]
public class ResConfig : ScriptableObject
{
    public int mapSize;
    public int resCount;
    public int extraSpace;
    public Mesh mesh;
    public Material material;
}