using System;
using UnityEngine;

/// <summary>
/// Missile collision FX pool Manager
/// </summary>
public class AmmoFXPoolManager : MonoBehaviour
{
    [SerializeField] private FXPoolSO ammoFX = default;
    [SerializeField, Range(1, 20)] private int poolSize = 10;

    [Header("Listening Channel")]
    [SerializeField] private FXRequestHandler ammoFXRecHandler = default;
    [SerializeField] private FXReturnHandler ammoFXRetHandler = default;

    private void OnEnable()
    {
        ammoFXRecHandler.onEventRaised += ShowFX;
        ammoFXRetHandler.onEventRaised += RetFX;
    }

    private void OnDisable()
    {
        ammoFXRecHandler.onEventRaised -= ShowFX;
        ammoFXRetHandler.onEventRaised -= RetFX;
        ammoFX.Disable();
    }

    private void Start()
    {
        ammoFX.PreWarm(poolSize);
        ammoFX.SetParent(this.transform);
    }

    private void ShowFX(Vector3 arg0)
    {
        var fx = ammoFX.Request();
        fx.transform.position = arg0;
        fx.Play();
    }

    private void RetFX(ParticleSystem arg0)
    {
        ammoFX.Return(arg0);
    }
}