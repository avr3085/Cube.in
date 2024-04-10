using System;
using UnityEngine;

/// <summary>
/// Handles all kernel UI
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

    private void ToggleLoadingUI(bool toggle)
    {
        loadingUI.SetActive(toggle);
    }
}