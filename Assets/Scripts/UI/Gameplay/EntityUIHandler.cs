using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EntityUIHandler : MonoBehaviour
{
    [SerializeField] private Entity entity;
    [SerializeField] private CannonController cannonController;
    [SerializeField] private Slider healthSlider = default;
    [SerializeField] private Slider levelSlider = default;
    [SerializeField] private TextMeshProUGUI levelText = default;

    private void OnEnable()
    {
        entity.OnEventRaised += RefreshUI;
    }

    private void OnDisable()
    {
        entity.OnEventRaised -= RefreshUI;
    }

    private void Start()
    {
        RefreshUI();
    }

    private void Update()
    {
        levelSlider.maxValue = cannonController.ReloadTime;
        levelSlider.value = cannonController.ElapsedReloadTime;
    }

    private void RefreshUI()
    {
        healthSlider.value = entity.Health;
        levelText.SetText("Level " + entity.Level.ToString());
        
    }
}