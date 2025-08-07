using System;
using UnityEngine;
using TMPro;

/// <summary>
/// Handles the Gameplay UI
/// -- Currently handling the activation and deactivation of the gameover UI
/// </summary>
public class MainLevelUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private TextMeshProUGUI scoreText; 
    [SerializeField] private TextMeshProUGUI killText; 

    [Header("Game Over UI Ref")]
    [SerializeField] private TextMeshProUGUI scoreTextGO; 
    [SerializeField] private TextMeshProUGUI killTextGO; 
    [SerializeField] private TextMeshProUGUI coinTextGO; 

    [SerializeField] private Entity playerEntity = default;

    [Header("Listening Channel"), SerializeField] private VoidEventListener gameOverRequestHandler = default;

    private void OnEnable()
    {
        gameOverRequestHandler.onEventRaised += ShowGameOverUI;
        playerEntity.OnEventRaised += RefreshUI;
    }

    private void OnDisable()
    {
        gameOverRequestHandler.onEventRaised -= ShowGameOverUI;
        playerEntity.OnEventRaised -= RefreshUI;
    }

    private void Start()
    {
        RefreshUI();
    }

    private void RefreshUI()
    {
        scoreText.text = playerEntity.Score.ToString();
        killText.text = playerEntity.KillCount.ToString();
    }

    private void ShowGameOverUI()
    {
        killTextGO.text = playerEntity.KillCount.ToString();
        scoreTextGO.text = playerEntity.Score.ToString();
        coinTextGO.text = (playerEntity.KillCount * 10).ToString();
        mainUI.SetActive(false);
        GameOverUI.SetActive(true);
    }
}