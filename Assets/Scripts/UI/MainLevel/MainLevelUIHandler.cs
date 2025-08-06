using UnityEngine;

/// <summary>
/// Handles the Gameplay UI
/// -- Currently handling the activation and deactivation of the gameover UI
/// </summary>
public class MainLevelUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject GameOverUI;

    [Header("Listening Channel"), SerializeField] private VoidEventListener gameOverRequestHandler = default;

    private void OnEnable()
    {
        gameOverRequestHandler.onEventRaised += ShowGameOverUI;
    }

    private void OnDisable()
    {
        gameOverRequestHandler.onEventRaised -= ShowGameOverUI;
    }

    private void ShowGameOverUI()
    {
        mainUI.SetActive(false);
        GameOverUI.SetActive(true);
    }
}