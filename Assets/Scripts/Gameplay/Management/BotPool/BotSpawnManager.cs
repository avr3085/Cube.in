using UnityEngine;

public class BotSpawnManager : MonoBehaviour
{
    [SerializeField] private BotPoolSO botPool = default;
    [SerializeField, Range(1, 10)] private int poolSize = 10;

    [Header("Spawn Settings")]
    [SerializeField, Range(1, 10)] private int initialSpawnAmount = 5; 
    [SerializeField, Range(1, 20)] private int sphereRadius = 5;
    [SerializeField] private LayerMask lMask;
    [SerializeField] private Transform[] spawnPoints;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
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
}