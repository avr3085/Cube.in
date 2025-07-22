using UnityEngine;

public class FXController : MonoBehaviour
{
    [Header("Boradcasting Channel")]
    [SerializeField] private FXReturnHandler fxReturnHandler = default;

    void OnParticleSystemStopped()
    {
        fxReturnHandler?.Raise(GetComponent<ParticleSystem>());
    }
}