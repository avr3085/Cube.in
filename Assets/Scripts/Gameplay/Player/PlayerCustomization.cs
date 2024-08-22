using UnityEngine;

public class PlayerCustomization : MonoBehaviour
{
    [SerializeField] private Material playerMat;
    [SerializeField] private TrailRenderer playerTrailRenderer;

    [Header("Data Channel")]
    [SerializeField] private InventoryItems inventoryItems = default;

    private void Start()
    {
        // playerMat.SetTexture()
    }
}