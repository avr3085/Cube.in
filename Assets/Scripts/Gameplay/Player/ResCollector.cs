using UnityEngine;
using System.Collections.Generic;
using Misc;

/// <summary>
/// Resource collection handler
/// </summary>
public class ResCollector : MonoBehaviour, IResCollector
{
    [Tooltip("Toggle's AABB for uniform grid collision check.")]

    private int currentHash = -1;
    private Vector3 Position => new Vector3(transform.position.x, 0f, transform.position.z);
    private IEnumerable<int> hashArray;

    private void Update()
    {

    #if UNITY_EDITOR
        ///For debug only
        Vector2 cPos = new Vector2(Position.x, Position.z);
        DrawDebugCube(cPos);

    #endif

        Vector3 mPos = new Vector3(Position.x - 0.5f, 0f, Position.z - 0.5f);
        if(currentHash != mPos.ToHash())
        {
            currentHash = mPos.ToHash();
            hashArray = Position.ToBBoxHash();// Use strategy pattern, if possible
            // hashArray = Position.ToMagBBoxHash(); // using Magnet method

            // Debug.Log("For Debug");
            // foreach(var val in hashArray)
            // {
            //     Debug.Log(val);
            // }
        }

        if(hashArray == null) return;

        foreach(int hashKey in hashArray)
        {
            ResFactoryManager.Instance.CheckCollision(hashKey, Position, this);
        }
    }

    /// <summary>
    /// A CallBack method, gets called every time we collect an item.
    /// [Read the class which is raising this method]
    /// </summary>
    /// <param name="resType"></param>
    public void OnResCollected(ResType resType)
    {
        // Raised when an item is collected
    }

    private void DrawDebugCube(Vector2 pos)
    {
        //this code will draw a debug cube around the player
        Debug.DrawLine(new Vector3(pos.x + 0.5f, 0f, pos.y + 0.5f), new Vector3(pos.x - 0.5f, 0f, pos.y + 0.5f), Color.red);
        Debug.DrawLine(new Vector3(pos.x - 0.5f, 0f, pos.y + 0.5f), new Vector3(pos.x - 0.5f, 0f, pos.y - 0.5f), Color.red);
        Debug.DrawLine(new Vector3(pos.x - 0.5f, 0f, pos.y - 0.5f), new Vector3(pos.x + 0.5f, 0f, pos.y - 0.5f), Color.red);
        Debug.DrawLine(new Vector3(pos.x + 0.5f, 0f, pos.y - 0.5f), new Vector3(pos.x + 0.5f, 0f, pos.y + 0.5f), Color.red);

    }

}