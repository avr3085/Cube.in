using System;
using UnityEngine;

/// <summary>
/// Explosion Fx pool Manager
/// </summary>
public class ExpFXPoolManager : MonoBehaviour
{
    [SerializeField] private FXPoolSO expFX = default;
    [SerializeField, Range(1, 20)] private int poolSize = 10;

    [Header("Listening Channel")]
    [SerializeField] private FXRequestHandler explosionRequestHandler = default;
    [SerializeField] private FXReturnHandler explosionReturnHandler = default;

    private void OnEnable()
    {
        explosionRequestHandler.onEventRaised += ShowFX;
        explosionReturnHandler.onEventRaised += ReturnFX;
    }

    private void OnDisable()
    {
        explosionRequestHandler.onEventRaised -= ShowFX;
        explosionReturnHandler.onEventRaised -= ReturnFX;
        expFX.Disable();
    }

    private void Start()
    {
        expFX.PreWarm(poolSize);
        expFX.SetParent(this.transform);
    }

    private void ShowFX(Vector3 arg0)
    {
        var fx = expFX.Request();
        fx.transform.position = arg0;
        fx.Play();
    }

    private void ReturnFX(ParticleSystem arg0)
    {
        expFX.Return(arg0);
    }
}