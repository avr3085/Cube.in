using System;
using UnityEngine;
using UnityEngine.Events;

public class MissilePoolManager : MonoBehaviour
{
    [Header("Pool Array"), Tooltip("Make Sure to add Bomb Type in Sorted manner. Ex - Add Type A before Type B.")]
    [SerializeField] private MissilePoolSO[] missilePoolArray;
    [SerializeField, Range(5, 20)] private int poolArraySize = 10;

    [Header("Listening Channel")]
    [SerializeField] private MissileRequestHandler missileRequestHandler = default;

    private void OnEnable()
    {
        missileRequestHandler.onEventRaised += RequestMissile;
    }

    private void OnDisable()
    {
        for(int i = 0; i < missilePoolArray.Length; i++)
        {
            var b = missilePoolArray[i];
            b.Disable();
        }

        missileRequestHandler.onEventRaised -= RequestMissile;
    }

    private void Start()
    {
        for(int i = 0; i < missilePoolArray.Length; i++)
        {
            var b = missilePoolArray[i];

            b.PreWarm(poolArraySize);
            b.SetParent(this.transform);
        }
    }

    private void RequestMissile(MissileType mType, Transform t)
    {
        MissileController requestedMissile = missilePoolArray[(int)mType].Request();
        requestedMissile.SetMissileDirection(mType, t);
        requestedMissile.onMissileHit += ReturnMissile;
    }

    private void ReturnMissile(MissileType arg0, MissileController arg1)
    {
        missilePoolArray[(int)arg0].Return(arg1);
        arg1.onMissileHit -= ReturnMissile;
    }
}