using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SoundEmitter : MonoBehaviour
{
    private AudioSource audioSource;
    public event UnityAction<SoundEmitter> onMusicStopEventRaised;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    /// <summary>
    /// Play Audio
    /// </summary>
    /// <param name="clip">Audio Clip</param>
    /// <param name="loop">Loop?</param>
    private void PlayAudioClip(AudioClip clip, bool loop){
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.time = 0f;
        audioSource.Play();

        if(!loop){
            StartCoroutine(StopMusicPlay(clip.length));
        }
    }

    /// <summary>
    /// Request to play Audio
    /// </summary>
    /// <param name="audioSO">Audio Data</param>
    public void PlayAudioRequest(AudioSO audioSO){
        PlayAudioClip(audioSO.audioClip, audioSO.loop);
    }

    /// <summary>
    /// Auto stop music
    /// </summary>
    /// <param name="audioLength">Seconds to wait before stopping</param>
    /// <returns></returns>
    private IEnumerator StopMusicPlay(float audioLength){
        yield return new WaitForSeconds(audioLength);
        onMusicStopEventRaised?.Invoke(this);
    } 
}