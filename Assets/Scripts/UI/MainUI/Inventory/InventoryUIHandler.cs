using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// System Inventory Ui handler
/// </summary>
public class InventoryUIHandler : MonoBehaviour
{
    [SerializeField] private Transform skinContent = default;
    [SerializeField] private SkinInventory skinInventory;
    [SerializeField] private Color selectedButtonColor;
    [SerializeField] private Color nonSelectedButtonColor;

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

    private void Start()
    {
        int currSelected = skinInventory.selectedSkin;
        DeactivateSkin(currSelected);
    }

    private void HandleInventoryRequests(int val)
    {
        ActivateSkin(skinInventory.selectedSkin);
        DeactivateSkin(val);
        skinInventory.selectedSkin = val;
    }

    private void ActivateSkin(int selectedSkin)
    {
        Transform selectedTrans = skinContent.GetChild(selectedSkin);
        Button button = selectedTrans.GetChild(1).GetComponent<Button>();
        button.enabled = true;
        button.GetComponent<Image>().color = nonSelectedButtonColor;
        button.GetComponentInChildren<TextMeshProUGUI>().text = "Use";
    }

    private void DeactivateSkin(int selectedSkin)
    {
        Transform selectedTrans = skinContent.GetChild(selectedSkin);
        Button button = selectedTrans.GetChild(1).GetComponent<Button>();
        button.enabled = false;
        button.GetComponent<Image>().color = selectedButtonColor;
        button.GetComponentInChildren<TextMeshProUGUI>().text = "Using";
    }
}