using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

/// <summary>
/// Boot
/// </summary>
public class BootManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private SceneSO kernel = default;
    [SerializeField] private SceneSO mainUI = default;

    [Header("Boradcasting Channel")]
    [SerializeField] private AssetReference mainUIRequestChannel = default;

    private void Start(){
        kernel.scene.LoadSceneAsync(LoadSceneMode.Additive, true).Completed += LoadSceneRequestChannelAsset;
    }

    private void LoadSceneRequestChannelAsset(AsyncOperationHandle<SceneInstance> handle)
    {
        mainUIRequestChannel.LoadAssetAsync<SceneRequestChannel>().Completed += LoadMainMenu;
    }

    private void LoadMainMenu(AsyncOperationHandle<SceneRequestChannel> handle)
    {
        handle.Result.Raise(mainUI, true);
        mainUIRequestChannel.ReleaseAsset();
        SceneManager.UnloadSceneAsync(0);
    }
}
