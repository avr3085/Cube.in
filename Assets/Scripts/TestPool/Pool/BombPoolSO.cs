using UnityEngine;

[CreateAssetMenu(fileName = "BombPool", menuName = "DataSO/Pool/BombPool")]
public class BombPoolSO : ComponentPool<Bomb>
{
    [SerializeField] private BombFactorySO bombFactorySO;

    protected override IFactory<Bomb> Factory 
    { 
        get => bombFactorySO;

        set
        {
            bombFactorySO = value as BombFactorySO;
        }
    }
}