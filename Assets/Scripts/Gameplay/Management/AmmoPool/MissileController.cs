using UnityEngine;
using UnityEngine.Events;

public class MissileController : MonoBehaviour
{
    [SerializeField, Range(1, 100)] private int moveSpeed = 15;
    public event UnityAction<MissileType, MissileController> onMissileHit;

    [Header("Broadcasting Channel")]
    [SerializeField] private FXRequestHandler ammoFXRecHandler = default;

    private Rigidbody rb;
    private MissileType mType;
    private const int EXP_CONST = 100;

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
        onMissileHit?.Invoke(mType, this);
        ammoFXRecHandler?.Raise(transform.position);

        var item = other.GetComponent<Entity>();
        if (item != this && item != null)
        {
            item.RBody.AddExplosionForce(EXP_CONST * ((int)mType + 1), transform.position, 2f, 0f, ForceMode.Impulse);
        }
    }
}