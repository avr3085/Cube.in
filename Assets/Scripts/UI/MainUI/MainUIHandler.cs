using UnityEngine;

/// <summary>
/// Main UI handler
/// </summary>
public class MainUIHandler : MonoBehaviour
{
    [Header("Data"), SerializeField] private SceneSO gameplay = default;

    [SerializeField] private SceneRequestChannel gameplayRequestChannel = default;

    public void RaiseRequest()
    {
        gameplayRequestChannel.Raise(gameplay, true);
    }
}
