using UnityEngine;
using UnityEngine.Events;

public class MissileController : MonoBehaviour
{
    [SerializeField, Range(1, 100)] private int moveSpeed = 15;
    public event UnityAction<MissileType, MissileController> onMissileHit;

    private Rigidbody rb;
    private MissileType mType;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetMissileDirection(MissileType missileType, Transform t)
    {
        transform.position = t.position;
        transform.rotation = Quaternion.LookRotation(t.forward);
        rb.velocity = t.forward * moveSpeed;
        mType = missileType;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        onMissileHit?.Invoke(mType, this);
    }
}