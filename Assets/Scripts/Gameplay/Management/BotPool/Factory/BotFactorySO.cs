using UnityEngine;

[CreateAssetMenu(fileName = "BotFactory", menuName = "DataSO/Factory/BotFactory")]
public class BotFactorySO : FactorySO<BotAIController>
{
    [SerializeField] private BotAIController botPrefab = default;
    
    public override BotAIController Create()
    {
        return Instantiate(botPrefab);
    }
}