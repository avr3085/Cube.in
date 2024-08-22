using UnityEngine;

public class InventoryUIHandler : MonoBehaviour
{
    [SerializeField] private InventoryItems inventoryItems;

    [Header("Listening Channel")]
    [SerializeField] private IntEventListener inventoryRequestHandler = default;

    private void OnEnable()
    {
        inventoryRequestHandler.onEventRaised += HandleInventoryRequests;
    }

    private void OnDisable()
    {
        inventoryRequestHandler.onEventRaised -= HandleInventoryRequests;
    }

    private void HandleInventoryRequests(int val)
    {
        inventoryItems.selectedItem = val;
    }
}