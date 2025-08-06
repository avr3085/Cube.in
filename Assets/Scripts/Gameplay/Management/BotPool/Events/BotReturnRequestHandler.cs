using UnityEngine;

/// <summary>
/// Returns bot to the pool, when bot dies
/// </summary>
[CreateAssetMenu(fileName = "BotReturnHandler", menuName = "EventSO/BotReturnHandler")]
public class BotReturnRequestHandler : BaseEventSO<BotAIController> {}