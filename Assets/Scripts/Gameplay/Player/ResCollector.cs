using UnityEngine;
using Misc;

/// <summary>
/// Resource collection handler
/// </summary>
public class ResCollector : MonoBehaviour, IResCollector
{
    [Tooltip("Toggle's AABB for uniform grid collision check.")]
    [SerializeField] private bool debug = false;

    private void Update()
    {
        if(debug)
        {
            Vector2 cPos = new Vector2(transform.position.x, transform.position.z);
            DrawDebugCube(cPos);
        }

        var hahsArray = transform.position.ToBBoxHash();
        foreach(int hashKey in hahsArray)
        {
            ResFactoryManager.Instance.CheckCollision(hashKey, transform.position, this);
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