using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private LayerMask lMask;

    public Vector3 Position => transform.position;

    public bool IsSafeSpawnPoint()
    {
        if (Physics.CheckSphere(transform.position, 5f, lMask))
        {
            return false;
        }
        return true;
    }
}