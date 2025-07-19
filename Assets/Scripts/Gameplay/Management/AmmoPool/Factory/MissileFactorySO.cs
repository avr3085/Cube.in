using UnityEngine;

[CreateAssetMenu(fileName = "MissileFactory", menuName = "DataSO/Factory/Missile")]
public class MissileFactorySO : FactorySO<MissileController>
{
    [SerializeField] private MissileController missilePrefab = default;

    public override MissileController Create()
    {
        return Instantiate(missilePrefab);
    }
}