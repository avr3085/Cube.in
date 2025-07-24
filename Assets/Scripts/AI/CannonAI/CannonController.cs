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
    private Vector3 lookDirection;
    private int reloadTime;

    public float ElapsedReloadTime => elapsedReloadTime;
    public int ReloadTime => reloadTime;

    private void Awake()
    {
        reloadTime = Random.Range(2, 10);
    }

    private void Update()
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
                        lookDirection = item.Position - controller.Position;
                        transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
                        missileRequestHandler.Raise(controller.ActiveMissileType, nozzle);
                        PlayParticle();
                        break;
                    }
                }
                reloadTime = Random.Range(2, 10);
                elapsedReloadTime = 0f;
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