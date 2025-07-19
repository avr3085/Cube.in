using UnityEngine;

[CreateAssetMenu(fileName = "MissilePool", menuName = "DataSO/Pool/Missile")]
public class MissilePoolSO : ComponentPool<MissileController>
{
    [SerializeField] private MissileFactorySO missileFactorySO;
    [SerializeField] private MissileType bombType = MissileType.Missile;

    protected override IFactory<MissileController> Factory 
    { 
        get => missileFactorySO;

        set
        {
            missileFactorySO = value as MissileFactorySO;
        }
    }
}