using UnityEngine;

/// <summary>
/// FX callback controller. will act when the particle system stops emmiting
/// </summary>
public class FXController : MonoBehaviour
{
    [Header("Boradcasting Channel")]
    [SerializeField] private FXReturnHandler fxReturnHandler = default;

    void OnParticleSystemStopped()
    {
        fxReturnHandler?.Raise(GetComponent<ParticleSystem>());
    }
}