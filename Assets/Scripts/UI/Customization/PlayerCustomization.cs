using JetBrains.Annotations;
using UnityEngine;

public class PlayerCustomization : MonoBehaviour
{
    [SerializeField] private Renderer bodyRenderer;
    [SerializeField] private Renderer cannonRenderer;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private SkinInventory skinInventory = default;

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

    private void UpdateSkin(int val)
    {
        SkinSO currentSkin = skinInventory.GetSkin(val);
        bodyRenderer.material.mainTexture = currentSkin.texture;
        cannonRenderer.material.mainTexture = currentSkin.texture;
        trailRenderer.colorGradient = currentSkin.trailColor;
    }
}