using UnityEngine;
using UnityEngine.Events;

// this script will be added to the bomb itself, will act when it hits the ground

public class Bomb : MonoBehaviour
{
    [SerializeField] private BombType bType = BombType.TypeA;

    public event UnityAction<Bomb, BombType> onBombHit;
    /// Add code for the missile to move forward


    private void OnTriggerEnter(Collider other)
    {  
        onBombHit?.Invoke(this, bType);
    }
}