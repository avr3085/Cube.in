using MiscUtils;
using UnityEngine;

public class BotAI : MonoBehaviour
{
    // [Header("Bot Properties"), SerializeField, Range(1, 100)] private int moveSpeed = 10;
    // [SerializeField, Range(1, 100)] private int rotSpeed = 10;
    // [SerializeField, Range(1,50)] private int mapSize = 5;
    // [SerializeField, Range(0.1f, 2f)] private float distanceOffset = 0.5f;

    // private Vector2 rndPos = Vector2.zero;
    // private Transform tf;
    // private Vector2 GetRandom => new Vector2(Random.Range( -mapSize, mapSize), Random.Range(-mapSize, mapSize));

    // private void Start()
    // {
    //     tf = this.transform;
    //     rndPos = GetRandom;
    // }

    // private void Update()
    // {
    //     var len = Vector2.Distance(tf.position.ToV2(), rndPos);
    //     if(len < distanceOffset)
    //     {
    //         rndPos = GetRandom;
    //     }else
    //     {
    //         tf.Translate(moveSpeed * Time.deltaTime * Vector2.right);
    //     }

    //     Rotate(rndPos);
    // }

    // private void Rotate(Vector2 pos)
    // {
    //     var dir = pos - tf.position.ToV2();
    //     var angle = Mathf.Atan2(dir.y, dir.x);
    //     Quaternion newRotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg);
    //     tf.rotation = Quaternion.Slerp(tf.rotation, newRotation, rotSpeed * Time.deltaTime);
    // }

    // #region Visual Debug

    // private void OnDrawGizmos()
    // {
    //     if(!Application.isPlaying) return;

    //     Gizmos.color = Color.green;
    //     Gizmos.DrawSphere(rndPos, 0.3f);

    //     /// Drawing a ray from the player to the direction of travel
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawRay(tf.position.ToV2(), (rndPos - tf.position.ToV2()).normalized);
    // }

    // #endregion
}