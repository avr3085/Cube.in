using UnityEngine;

[RequireComponent(typeof(PlayerCustomization))]
public class PlayerCustomization : SkinCustomization
{
    [Header("Listening Channel")]
    [SerializeField] private IntEventListener inventoryRequestHandler = default;

    private void OnEnable()
    {
        inventoryRequestHandler.onEventRaised += UpdateActiveSkin;
    }

    private void OnDisable()
    {
        inventoryRequestHandler.onEventRaised -= UpdateActiveSkin;
    }

    private void UpdateActiveSkin(int val)
    {
        UpdateSkin(val);
    }

    private void Start()
    {
        UpdateSkin(skinInventory.selectedSkin);
    }
}