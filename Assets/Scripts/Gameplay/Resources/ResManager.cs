using UnityEngine;

/// <summary>
/// Add/Remove/Draw Resources on the screen
/// </summary>
public class ResManager : MonoBehaviour
{
    private void Start()
    {
        ResFactoryManager.Instance.Initialize();
    }

    private void Update()
    {
        ResFactoryManager.Instance.Draw();
    }

    private void OnDestroy()
    {
        ResFactoryManager.Instance.DeInitialize();
    }
}