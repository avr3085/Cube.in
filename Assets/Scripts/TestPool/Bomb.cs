using UnityEngine;
using UnityEngine.Events;

public class Bomb : MonoBehaviour
{
    public event UnityAction<Bomb> onBombHit;
    
    private void OnTriggerEnter(Collider other)
    {
        //code here   
        onBombHit?.Invoke(this);
    }
}