using UnityEngine;

[CreateAssetMenu(fileName = "FXPool", menuName = "DataSO/Pool/FXPool")]
public class FXPoolSO : ComponentPool<ParticleSystem>
{
    [SerializeField] private FXFactorySO fx = default;
    
    protected override IFactory<ParticleSystem> Factory
    {
        get => fx;
        set
        {
            fx = value as FXFactorySO;
        }
    }
}