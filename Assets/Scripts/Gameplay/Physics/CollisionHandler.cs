using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private int lineLength = 1;

    private void OnDrawGizmos()
    {
        if(!Application.isPlaying) return;

        var forPos = (transform.forward *lineLength) +  transform.position;

        Gizmos.DrawRay(transform.position, transform.forward);
        Gizmos.DrawSphere(forPos, 0.3f);

        Gizmos.color = Color.green;
        // var val = Raster.Line(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.z), Mathf.FloorToInt(forPos.x), Mathf.FloorToInt(forPos.z));
        // foreach (var i in val)
        // {
        //     var hash  = new Vector3(i.x, 0f, i.y);
        //     //remove item if at certain distance
        //     if(ResManager.Instance.HasKey((int) hash.GetHash()))
        //     {
        //         ResManager.Instance.RemoveItem((int) hash.GetHash());
        //     }
        //     Gizmos.DrawSphere(new Vector3(i.x, 0f , i.y), 0.1f);
        //     DebugCube(i.x, i.y);
        // }
    }

    private void DebugCube(int x, int y)
    {
        Gizmos.DrawLine(new Vector3(x, 0f, y), new Vector3(x + 1, 0f, y));
        Gizmos.DrawLine(new Vector3(x + 1, 0f, y), new Vector3(x + 1, 0f, y + 1));
        Gizmos.DrawLine(new Vector3(x + 1, 0f, y + 1), new Vector3(x, 0f, y + 1));
        Gizmos.DrawLine(new Vector3(x, 0f, y + 1), new Vector3(x, 0f, y));
    }
}