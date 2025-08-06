using UnityEngine;

/// <summary>
/// This calss is responsibel for making bot pool and spawning them into random position in the game
/// </summary>
public class BotSpawnManager : MonoBehaviour
{
    [SerializeField] private BotPoolSO botPool = default;
    [SerializeField, Range(1, 10)] private int poolSize = 10;

    [Header("Spawn Settings")]
    [SerializeField, Range(1, 10)] private int initialSpawnAmount = 5;
    [SerializeField, Range(1, 20)] private int sphereRadius = 5;
    [SerializeField] private Transform[] spawnPoints;

    [Header("Listening Channel")]
    [SerializeField] private BotReturnRequestHandler botReturnRequest = default;

    private void OnEnable()
    {
        botReturnRequest.onEventRaised += ReturnBot;
    }

    private void OnDisable()
    {
        botReturnRequest.onEventRaised -= ReturnBot;
        botPool.Disable();
    }

    private void Start()
    {
        botPool.PreWarm(poolSize);
        botPool.SetParent(this.transform);

        SpawnBots(initialSpawnAmount);
    }

    private void SpawnBots(int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            int randInt = Random.Range(0, spawnPoints.Length);
            var bot = botPool.Request();
            bot.SetPosition = spawnPoints[randInt].position;
            //set the bots to random level
        }
    }

    private void ReturnBot(BotAIController bot)
    {
        botPool.Return(bot);
        SpawnBots();
    }
}