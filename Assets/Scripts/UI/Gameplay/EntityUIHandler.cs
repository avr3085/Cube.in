using UnityEngine;
using UnityEngine.UI;

public class EntityUIHandler : MonoBehaviour
{
    [SerializeField] private Entity entity;
    [SerializeField] private Slider healthSlider = default;
    [SerializeField] private Slider levelSlider = default;

    private void OnEnable()
    {
        entity.OnEventRaised += ActNow;
    }

    private void OnDisable()
    {
        entity.OnEventRaised -= ActNow;
    }

    private void ActNow()
    {
        UpdateUI();
    }

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        healthSlider.value = entity.Health;
        levelSlider.value = 100;
    }
}