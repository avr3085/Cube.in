using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// Loads Main menu and unload main level and gameplayUI
/// </summary>
public class GameplaySceneManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private SceneSO mainUI = default;

    [Header("Broadcasting Channel")]
    [SerializeField] private AssetReference mainUIRequestChannel = default;

    [Header("Listening Channel")]
    [SerializeField] private VoidEventListener exitEventListener = default;

    private void OnEnable(){
        exitEventListener.onEventRaised += LoadMainUIAsset;
    }

    private void OnDisable(){
        // mainUIRequestChannel.ReleaseAsset(); // releasing asset before loading another scene
        exitEventListener.onEventRaised -= LoadMainUIAsset;
    }

    private void LoadMainUIAsset()
    {
        mainUIRequestChannel.LoadAssetAsync<SceneRequestChannel>().Completed += RequestMainUI;
    }

    private void RequestMainUI(AsyncOperationHandle<SceneRequestChannel> handle)
    {
        handle.Result.Raise(mainUI, true);
    }
}
