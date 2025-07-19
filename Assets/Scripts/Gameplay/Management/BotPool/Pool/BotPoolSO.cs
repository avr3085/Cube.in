using UnityEngine;

[CreateAssetMenu(fileName = "BotPool", menuName = "DataSO/Pool/BotPool")]
public class BotPoolSO : ComponentPool<BotAIController>
{
    [SerializeField] private BotFactorySO factory = default;
    
    protected override IFactory<BotAIController> Factory
    {
        get => factory;
        set
        {
            factory = value as BotFactorySO;
        }
    }
}