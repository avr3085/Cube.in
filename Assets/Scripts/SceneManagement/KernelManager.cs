using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

/// <summary>
/// presistance manager
/// </summary>
public class KernelManager : MonoBehaviour
{
    [Header("Data Channel")]
    [SerializeField] private SceneSO mainLevel = default;

    [Header("Broadcasting Channel")]
    [SerializeField] private BoolEventListener toggleLoadingUIListener = default;

    [Header("Listening Channel")]
    [SerializeField] private SceneRequestChannel mainUIRequestChannel = default;
    [SerializeField] private SceneRequestChannel gameplayRequestChannel = default;

    private SceneInstance currentSceneInstance = new SceneInstance();
    private SceneInstance currentLevelInstance = new SceneInstance();

    private bool isSceneLoading = false;
    private const string MAIN_UI = "MainUI";
    private const string GAMEPLAY = "Gameplay";

    private void OnEnable(){
        mainUIRequestChannel.onEventRaised += LoadMainUI;
        gameplayRequestChannel.onEventRaised += LoadGameplay;
    }

    private void OnDisable(){
        mainUIRequestChannel.onEventRaised -= LoadMainUI;
        gameplayRequestChannel.onEventRaised -= LoadGameplay;
    }

    private void LoadMainUI(SceneSO sceneSO, bool val)
    {
        if(isSceneLoading){
            return;
        }

        isSceneLoading = true;
        toggleLoadingUIListener.Raise(isSceneLoading);
        if(currentSceneInstance.Scene.isLoaded && currentSceneInstance.Scene != null){
            UnloadScene();
        }
        LoadScene(sceneSO);
    }

    private void LoadGameplay(SceneSO sceneSO, bool val)
    {
        if(isSceneLoading){
            return;
        }

        isSceneLoading = true;
        toggleLoadingUIListener.Raise(isSceneLoading);
        if(currentSceneInstance.Scene.isLoaded && currentSceneInstance.Scene != null){
            UnloadScene();
        }
        LoadScene(sceneSO);   
    }

    private void OnMainUILoaded(AsyncOperationHandle<SceneInstance> handle)
    {
        currentSceneInstance = handle.Result;
        isSceneLoading = false;
        toggleLoadingUIListener.Raise(isSceneLoading);
    }

    private void OnGameplayLoaded(AsyncOperationHandle<SceneInstance> handle)
    {
        currentSceneInstance = handle.Result;
        mainLevel.scene.LoadSceneAsync(LoadSceneMode.Additive, true).Completed += OnMainLevelLoaded;
    }

    private void OnMainLevelLoaded(AsyncOperationHandle<SceneInstance> handle)
    {
        currentLevelInstance = handle.Result;
        isSceneLoading = false;
        toggleLoadingUIListener.Raise(isSceneLoading);
    }

    private void LoadScene(SceneSO scene){
        if(scene.SceneName == MAIN_UI){
            scene.scene.LoadSceneAsync(LoadSceneMode.Additive, true).Completed += OnMainUILoaded;
        }else if(scene.SceneName == GAMEPLAY){
            scene.scene.LoadSceneAsync(LoadSceneMode.Additive, true).Completed += OnGameplayLoaded;
        }
    }

    private void UnloadScene(){
        if(currentLevelInstance.Scene.isLoaded){
            SceneManager.UnloadSceneAsync(currentLevelInstance.Scene);
        }

        SceneManager.UnloadSceneAsync(currentSceneInstance.Scene);
    }
}
