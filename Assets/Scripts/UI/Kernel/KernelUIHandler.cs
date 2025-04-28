using System;
using UnityEngine;

/// <summary>
/// Kernel UI handler
/// </summary>
public class KernelUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject loadingUI = default;

    [Header("Listening Channel"), SerializeField] private BoolEventListener toggleLoadingUIListener = default;

    private void OnEnable()
    {
        toggleLoadingUIListener.onEventRaised += ToggleLoadingUI;
    }

    private void OnDisable()
    {
        toggleLoadingUIListener.onEventRaised -= ToggleLoadingUI;
    }

    /// <summary>
    /// Toggle Loading UI on/off while scene loading is in progress
    /// </summary>
    /// <param name="toggle"></param>
    private void ToggleLoadingUI(bool toggle)
    {
        loadingUI.SetActive(toggle);
    }
}