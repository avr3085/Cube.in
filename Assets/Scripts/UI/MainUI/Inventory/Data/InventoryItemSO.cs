using UnityEngine;

/// <summary>
/// Inventory item data
/// </summary>
[CreateAssetMenu(fileName = "InventoryItem", menuName = "DataSO/UI/InventoryItem")]
public class InventoryItemSO : ScriptableObject
{
    public Sprite refImage;
    public Texture2D texture;
    public Gradient trailColor;
}