using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField, Range(0,180)] private int viewAngle = 45;
    [SerializeField, Range(1, 10)] private int lineLength = 1;

    private float rightOut, leftOut;
    private Vector3 rightDeg, leftDeg;

    private void Update()
    {
        rightOut = viewAngle + transform.eulerAngles.y;
        rightDeg = new Vector3(transform.position.x + Mathf.Sin(rightOut * Mathf.Deg2Rad) *  lineLength, 0f, transform.position.z + Mathf.Cos(rightOut * Mathf.Deg2Rad) * lineLength);

        leftOut = (360 - viewAngle) + transform.eulerAngles.y;
        leftDeg = new Vector3(transform.position.x + Mathf.Sin(leftOut * Mathf.Deg2Rad) * lineLength, 0f, transform.position.z + Mathf.Cos(leftOut * Mathf.Deg2Rad) *  lineLength);

        var raster = Raster.Line(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.z), 
            Mathf.FloorToInt(rightDeg.x), Mathf.FloorToInt(rightDeg.z), 
            Mathf.FloorToInt(leftDeg.x), Mathf.FloorToInt(leftDeg.z));

        // foreach (var i in raster)
        // {
        //     var hash  = new Vector3(i.x, 0f, i.y);
        //     //remove item if at certain distance
        //     if(ResManager.Instance.HasKey((int) hash.GetHash()))
        //     {
        //         ResManager.Instance.RemoveItem((int) hash.GetHash());
        //     }
        //     // DebugCube(i.x, i.y);
        // }
    }

    // private void DebugCube(int x, int y)
    // {
    //     Gizmos.DrawLine(new Vector3(x, 0f, y), new Vector3(x + 1, 0f, y));
    //     Gizmos.DrawLine(new Vector3(x + 1, 0f, y), new Vector3(x + 1, 0f, y + 1));
    //     Gizmos.DrawLine(new Vector3(x + 1, 0f, y + 1), new Vector3(x, 0f, y + 1));
    //     Gizmos.DrawLine(new Vector3(x, 0f, y + 1), new Vector3(x, 0f, y));
    // }
}