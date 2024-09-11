using UnityEngine;
using MiscUtils;

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
        

        // rightOut = viewAngle + transform.eulerAngles.y;
        // rightDeg = new Vector3(transform.position.x + Mathf.Sin(rightOut * Mathf.Deg2Rad) *  lineLength, 0f, transform.position.z + Mathf.Cos(rightOut * Mathf.Deg2Rad) * lineLength);

        // leftOut = (360 - viewAngle) + transform.eulerAngles.y;
        // leftDeg = new Vector3(transform.position.x + Mathf.Sin(leftOut * Mathf.Deg2Rad) * lineLength, 0f, transform.position.z + Mathf.Cos(leftOut * Mathf.Deg2Rad) *  lineLength);

        // var raster = Raster.Line(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.z), 
        //     Mathf.FloorToInt(rightDeg.x), Mathf.FloorToInt(rightDeg.z), 
        //     Mathf.FloorToInt(leftDeg.x), Mathf.FloorToInt(leftDeg.z));

        // foreach (var i in raster)
        // {
        //     var hash  = new Vector3(i.x, 0f, i.y).GetHash();
        //     // remove item if at certain distance
        //     if(ResFactoryManager.Instance.ContainsKey(hash))
        //     {
        //         ResFactoryManager.Instance.RequestCollect(hash, this);
        //     }
        // }
    }

    public void OnCollect(ResType resType)
    {
        //called when an item is collected
        // Debug.Log(resType.ToString() + " Item collected!");
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