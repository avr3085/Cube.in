using UnityEngine;
using Misc;

/// <summary>
/// This class is like a bridge between Resource collector and Resource factories.
/// </summary>
public class ResFactoryManager : MonoBehaviour
{
    [SerializeField] private ResFactorySO[] resFactories = default;

    public static ResFactoryManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        foreach (var factory in resFactories)
        {
            factory.Init();
        }
    }

    private void LateUpdate()
    {
        foreach (var factory in resFactories)
        {
            factory.DrawMesh();
        }
    }

    private void OnDisable()
    {
        foreach (var factory in resFactories)
        {
            factory.DeInit();
        }
    }

    /// <summary>
    /// Bridge between Caller and reciever
    /// Remove resource if caller collides with it
    /// </summary>
    /// <param name="hashKey"></param>
    /// <param name="pos"></param>
    /// <param name="resCollector">Caller</param>
    public void CollisionCheck(int hashKey, Vector3 pos, IResCollector resCollector)
    {
        foreach (var factory in resFactories)
        {
            if (factory.ContainsKey(hashKey))
            {
                factory.CollisionCheck(hashKey, pos, resCollector);
            }
        }
    }

    /// <summary>
    /// Returns the best Nerest Resorce Box position from the player position
    /// </summary>
    /// <param name="hashKey">Hash key for the uniform grid</param>
    /// <param name="pos">Current Player position</param>
    /// <returns>Best Nearest Resource position</returns>
    public Vector3 NearestRes(int hashKey, Vector3 pos, ResType resType = ResType.Edible)
    {
        foreach (var factory in resFactories)
        {
            if (factory.ResourceType == resType)
            {
                return factory.NearestNodeCheck(hashKey, pos);
            }
        }

        return resFactories[0].NearestNodeCheck(hashKey, pos);
    }

    public bool ContainsKey(int key, ResType resType = ResType.Edible)
    {
        foreach (var factory in resFactories)
        {
            if (factory.ResourceType == resType)
            {
                return factory.ContainsKey(key);
            }
        }

        return resFactories[0].ContainsKey(key);
    }
}