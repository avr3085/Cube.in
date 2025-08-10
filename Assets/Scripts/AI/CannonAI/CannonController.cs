using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField] private Entity controller;
    [SerializeField] private Transform nozzle;
    [SerializeField] private ParticleSystem pSystem;

    [Header("Data Channel"), SerializeField] private AudioSO fireAudioSO = default;

    [Header("Broadcasting Channel")]
    [SerializeField] private MissileRequestHandler missileRequestHandler = default;
    [SerializeField] private AudioClipRequestHandler audioClipRequestHandler = default;

    private float elapsedReloadTime = 0f;
    private float shootWait = 0f;
    private int shotToFire = 10;
    private Vector3 lookDirection;
    private int reloadTime;
    private Entity target;

    public float ElapsedReloadTime => elapsedReloadTime;
    public int ReloadTime => reloadTime;

    private void Awake()
    {
        reloadTime = (int)controller.ActiveMissileType + 1;
    }

    private void Start()
    {
        target = null;
    }

    private void Update()
    {
        if (target == null)
        {
            LocateTarget();
        }
        else
        {
            ShootTarget();
        }

    }

    private void ShootTarget()
    {
        if (shotToFire > 0)
        {
            if (shootWait < 0.5f)
            {
                shootWait += Time.deltaTime;
            }
            else
            {
                lookDirection = target.Position - controller.Position;
                transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
                missileRequestHandler.Raise(controller, nozzle);
                PlayParticle();
                shootWait = 0f;
                shotToFire--;
            }
        }
        else
        {
            shotToFire = 10;
            target = null;
            reloadTime = (int)controller.ActiveMissileType + 1;
            elapsedReloadTime = 0f;
        }
    }

    private void LocateTarget()
    {
        if (elapsedReloadTime < reloadTime)
        {
            elapsedReloadTime += Time.deltaTime;
        }
        else
        {
            int res = controller.CheckOverlapsBox();
            if (res > 1)
            {
                foreach (Collider col in controller.Colls)
                {
                    var item = col.GetComponent<Entity>();
                    if (item != controller)
                    {
                        target = item;
                        // lookDirection = item.Position - controller.Position;
                        // transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
                        // missileRequestHandler.Raise(controller, nozzle);
                        // PlayParticle();
                        // Shoot(item);
                        break;
                    }
                }

                // reloadTime = (int)controller.ActiveMissileType + 1;
                // elapsedReloadTime = 0f;
            }
        }
    }

    private void PlayParticle()
    {
        audioClipRequestHandler.Raise(fireAudioSO);
        pSystem.gameObject.SetActive(true);
        if (!pSystem.isPlaying) pSystem.Play();
    }
}