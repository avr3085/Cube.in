using UnityEngine;

/// <summary>
/// skin Inventory item data
/// </summary>
[CreateAssetMenu(fileName = "Skin", menuName = "DataSO/UI/Inventory/SkinSO")]
public class SkinSO : ScriptableObject
{
    public Texture2D texture;
    public Gradient trailColor;
}