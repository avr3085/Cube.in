using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private LayerMask lMask;
    
    public bool IsSafeSpanwnPoint()
    {
        if (Physics.CheckSphere(transform.position, 5f, lMask))
        {
            return false;
        }
        return true;
    }
}