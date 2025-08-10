using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles the missile movement and callbacks
/// </summary>
public class MissileController : MonoBehaviour
{
    [SerializeField, Range(1, 100)] private int moveSpeed = 15;
    public event UnityAction<MissileType, MissileController> onMissileHit;

    [Header("Data Channel"), SerializeField] private AudioSO explosionAudioSO = default;

    [Header("Broadcasting Channel")]
    [SerializeField] private FXRequestHandler ammoFXRecHandler = default;
    [SerializeField] private AudioClipRequestHandler audioClipRequestHandler = default;

    private Rigidbody rb;
    // private MissileType mType;
    private Entity missileOwner;
    private const int EXP_CONST = 50;
    private const int BASE_DAMAGE = 20;
    private int MissileToInt => (int)missileOwner.ActiveMissileType + 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Setting missile direction for shooting
    /// </summary>
    /// <param name="mEntity"></param>
    /// <param name="t"></param>
    public void SetMissileDirection(Entity mEntity, Transform t)
    {
        transform.position = t.position;
        transform.rotation = Quaternion.LookRotation(t.forward);
        rb.velocity = t.forward * moveSpeed;
        // mType = mEntity.ActiveMissileType;
        missileOwner = mEntity;
    }

    /// <summary>
    /// How the missile will act when hit
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        onMissileHit?.Invoke(missileOwner.ActiveMissileType, this); // returns to the pool manager after the hit
        ammoFXRecHandler?.Raise(transform.position); // pops explosion effect of the missile when it hit
        audioClipRequestHandler.Raise(explosionAudioSO); // missile hit sound effect

        var item = other.GetComponent<Entity>();
        if (item != this && item != null)
        {
            item.RBody.AddExplosionForce(EXP_CONST * MissileToInt, transform.position, 0.3f, 0f, ForceMode.Impulse); // Creating impulse effect
            item.TakeDamage(BASE_DAMAGE * MissileToInt); // this will add damage to the hit entity
            item.ResetLevel();
            if (item.Health <= 0)
            {
                // add the kill count and score as callback
                missileOwner.KillCount++;
                missileOwner.Score += 1000;
            }
        }
    }
}