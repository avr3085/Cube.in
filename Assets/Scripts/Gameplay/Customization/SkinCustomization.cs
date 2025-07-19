using UnityEngine;

/// <summary>
/// Player/Bot customization system
/// </summary>
public class SkinCustomization : MonoBehaviour
{
    [SerializeField] protected Renderer bodyRenderer;
    [SerializeField] protected Renderer cannonRenderer;
    [SerializeField] protected TrailRenderer trailRenderer;
    [SerializeField] protected SkinInventory skinInventory = default;

    protected void UpdateSkin(int val)
    {
        SkinSO currentSkin = skinInventory.GetSkin(val);
        bodyRenderer.material.mainTexture = currentSkin.texture;
        cannonRenderer.material.mainTexture = currentSkin.texture;
        trailRenderer.colorGradient = currentSkin.trailColor;
    }
}