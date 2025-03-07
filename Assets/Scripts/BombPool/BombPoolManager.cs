using System;
using UnityEngine;

public class BombPoolManager : MonoBehaviour
{
    [Header("Pool Array"), Tooltip("Make Sure to add Bomb Type in Sorted manner. Ex - Add Type A before Type B.")]
    [SerializeField] private BombPoolSO[] bombPoolArray;
    [SerializeField, Range(5, 20)] private int poolArraySize = 10;

    [Header("Listening Channel")]
    [SerializeField] private BombRequestHandler bombRequestHandler = default;

    private void OnEnable()
    {
        bombRequestHandler.onEventRaised += RequestBomb;
    }

    private void OnDisable()
    {
        for(int i = 0; i < bombPoolArray.Length; i++)
        {
            var b = bombPoolArray[i];
            b.Disable();
        }

        bombRequestHandler.onEventRaised -= RequestBomb;
    }

    private void Start()
    {
        for(int i = 0; i < bombPoolArray.Length; i++)
        {
            var b = bombPoolArray[i];

            b.PreWarm(poolArraySize);
            b.SetParent(this.transform);
        }
    }

    private void RequestBomb(BombType bType)
    {
        // var requestedType = (int)bType;
        BombPoolSO selectedPool = bombPoolArray[(int)bType];
        Bomb requestedBomb = selectedPool.Request();
        requestedBomb.onBombHit += OnBombHit;
    }

    private void OnBombHit(Bomb bomb, BombType bType)
    {
        BombPoolSO selectedPool = bombPoolArray[(int)bType];
        selectedPool.Return(bomb);
    }
}