using UnityEngine;

/// <summary>
/// Inventory Data
/// Contains inventory Ui related data array
/// </summary>
[CreateAssetMenu(fileName = "SkinInventory", menuName = "DataSO/UI/Inventory/SkinInventory")]
public class SkinInventory : ScriptableObject
{
    public int selectedSkin;
    public SkinSO[] skins;
    public SkinSO GetSkin() => skins[selectedSkin];
    public SkinSO GetSkin(int value) => skins[value];
}