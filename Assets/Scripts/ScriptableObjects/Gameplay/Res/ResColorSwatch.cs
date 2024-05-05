using UnityEngine;

[CreateAssetMenu(menuName ="DataSO/Gameplay/ColorSwatch", fileName ="ColorSwatch")]
public class ResColorSwatch : BaseSO
{
    public Color[] swatches;
    public int swatchCount => swatches.Length;
}