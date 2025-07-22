using UnityEngine;

[CreateAssetMenu(fileName = "FXFactory", menuName = "DataSO/Factory/FXFactory")]
public class FXFactorySO : FactorySO<ParticleSystem>
{
    [SerializeField] private ParticleSystem particle;

    public override ParticleSystem Create()
    {
        return Instantiate(particle);
    }
}