using UnityEngine;

public class ResCollector : MonoBehaviour, ICollect
{
    [SerializeField] private bool debug = false;
    private void Update()
    {
        //Making a collision box to check collision in cube
        if(debug)
        {
            Vector2 cPos = new Vector2(transform.position.x, transform.position.z);
            DrawDebugCube(cPos);
        }

        var collisionArray = BBoxCC.GetCollisionHash(transform.position);
        foreach (int i in collisionArray)
        {
            ResFactoryManager.Instance.CheckCollision(i, transform.position, this);
        }
    }

    public void OnCollect(ResType resType)
    {
        //called when an item is collected
        Debug.Log(resType.ToString() + " Item collected!");
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