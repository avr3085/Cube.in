using UnityEngine;

/// <summary>
/// Generates pool of sound emmiter
/// Responds to AudioRequestHandler Event
/// </summary>
public class SoundManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private AudioPoolSO audioPoolSO = default;
    [SerializeField] private int poolSize = 5;

    [Header("Listening Channel")]
    [SerializeField] private AudioClipRequestHandler audioClipRequestHandler = default;

    // TODO - add sound mixer related settings

    private void OnEnable()
    {
        audioClipRequestHandler.onEventRaised += RequestAudio;
    }

    private void OnDisable()
    {
        audioClipRequestHandler.onEventRaised -= RequestAudio;
        audioPoolSO.Disable();
    }

    private void Start()
    {
        audioPoolSO.PreWarm(poolSize);
        audioPoolSO.SetParent(this.transform);
    }

    private void RequestAudio(AudioSO val)
    {
        var item = audioPoolSO.Request();
        item.PlayAudioRequest(val);
        item.onMusicStopEventRaised += ReturnItemToPool;
    }

    private void ReturnItemToPool(SoundEmitter item)
    {
        audioPoolSO.Return(item);
        item.onMusicStopEventRaised -= ReturnItemToPool;
    }

}