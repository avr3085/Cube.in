using UnityEngine;

[CreateAssetMenu(menuName ="DataSO/Gameplay/ResConfig", fileName ="ResConfig")]
public class ResConfig : ScriptableObject
{
    public int extraSpace;
    public int entityCount;
    public int mapSize;
    public Material material;
    public Mesh mesh;
}