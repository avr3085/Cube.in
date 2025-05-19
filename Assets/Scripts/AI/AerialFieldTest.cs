using UnityEngine;
using Misc;
using System.Collections.Generic;

/// <summary>
/// This is the test class, and it should not be added to the final build
/// </summary>
public class AerialFieldTest : MonoBehaviour
{
    [SerializeField, Range(1,100)] private int visiblity = 2;
    [SerializeField, Range(1, 50)] private int mapSize = 5;

    private int currentHash = -1;
    // private IEnumerable<Vector3> hashArrayV3;
    private IEnumerable<Vector3> hashArrayPos;

    public Vector3 Pos => new Vector3(transform.position.x, 0f, transform.position.z);
    
    private void OnDrawGizmos()
    {
        // if(!Application.isPlaying) return;
        
        if(currentHash != Pos.ToHash())
        {
            currentHash = Pos.ToHash();
            // hashArrayV3 = Pos.ToBBoxHashV3();// Use strategy pattern, if possible -- use a bigger Range
            // hashArray = Position.ToMagBBoxHash(); // using Magnet method

            hashArrayPos = Pos.BoxVisionV3(visiblity, mapSize);
        }

        Gizmos.color = Color.green;
        foreach(var item in hashArrayPos)
        {
            Gizmos.DrawSphere(item, 0.2f);
        }
    }
}