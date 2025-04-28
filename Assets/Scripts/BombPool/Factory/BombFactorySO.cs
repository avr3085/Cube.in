using UnityEngine;

[CreateAssetMenu(fileName = "BombFactory", menuName = "DataSO/Factory/BombFactory")]
public class BombFactorySO : FactorySO<Bomb>
{
    [SerializeField] private Bomb bombPrefab = default;

    public override Bomb Create()
    {
        return Instantiate(bombPrefab);
    }
}