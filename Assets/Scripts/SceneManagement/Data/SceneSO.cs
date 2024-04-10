using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// Scene SO
/// </summary>
[CreateAssetMenu(fileName ="SceneSO", menuName ="DataSO/SceneSO")]
public class SceneSO : BaseSO
{
    public string SceneName;
    public AssetReference scene;
}
