using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItems", menuName = "DataSO/UI/InventoryItems")]
public class InventoryItems : ScriptableObject
{
    public int selectedItem;
    public InventoryItemSO[] items;
    public InventoryItemSO GetItem => items[selectedItem];
}