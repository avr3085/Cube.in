using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Transform[] spawnPoints;

    [Header("Listening Channel")]
    [SerializeField] private BotReturnRequestHandler botReturnRequest = default;

    private Queue<BotAIController> returnQ = new Queue<BotAIController>();

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
        }
    }

    private void ReturnBot(BotAIController bot)
    {
        returnQ.Enqueue(bot);
        botPool.Return(bot);

        StartCoroutine(WaitSpawn());
        // bot.InitData((MissileType)Random.Range(0, 5));
        // bot.ResetState();
        // SpawnBots();
    }

    private IEnumerator WaitSpawn()
    {
        yield return new WaitForSeconds(1f);

        if (returnQ.Count > 0)
        {
            var bot = returnQ.Dequeue();
            SpawnBots();
            bot.InitData((MissileType)Random.Range(0, 5));
            bot.ResetState();

            StartCoroutine(WaitSpawn());
        }
    }
}