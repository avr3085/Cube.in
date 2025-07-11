using UnityEngine;

public class BotCustomization : SkinCustomization
{
    private void Start()
    {
        int randSkin = Random.Range(0, skinInventory.skins.Length);
        UpdateSkin(randSkin);
    }
}