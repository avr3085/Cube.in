using UnityEngine;

/// <summary>
/// Input Handler
/// Handle all input related function here
/// </summary>

//Todo --> Joystick input is yet to be implemented
public class InputHandler : MonoBehaviour
{
    [Header("Broadcasting Channel")]
    [SerializeField] private IntEventListener inputAixsListener = default;
    private Vector2 mousePositon = Vector2.zero;

    private void Update(){
#if UNITY_EDITOR

        //Taking Input from arrow key
        if(Input.GetKey(KeyCode.RightArrow))
        {
            inputAixsListener.Raise(1);
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            inputAixsListener.Raise(-1);
        }

        // if(Input.GetMouseButtonDown(0)){
        //     mousePositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //     Vector2 finalPos = new Vector2(mousePositon.x + 4.5f, mousePositon.y + 4.5f);
        //     if(finalPos.x>=0 && finalPos.x<=9 && finalPos.y>=0 && finalPos.y<=9) mousePositionListener.Raise(finalPos);
        // }
#endif

#if UNITY_WEBGL
        // int currentFinger = -1;
        // foreach(Touch t in Input.touches){
        //     if(t.phase != TouchPhase.Ended && t.phase != TouchPhase.Canceled){
        //         currentFinger++;
        //     }else{
        //         return;
        //     }

        //     if(currentFinger == 0){
        //         if(t.phase == TouchPhase.Began){
        //             mousePositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //             Vector2 finalPos = new Vector2(mousePositon.x + 4.5f, mousePositon.y + 4.5f);
        //             if(finalPos.x>=0 && finalPos.x<=9 && finalPos.y>=0 && finalPos.y<=9) mousePositionListener.Raise(finalPos);
        //         }
        //     }else{
        //         return;
        //     }
        // }
#endif

// TODO : make sure to add input function for andorid platform

}


// #region DebugRegion
//     private void OnDrawGizmos(){
//          for Debug Purpose
//     }
// #endregion

}