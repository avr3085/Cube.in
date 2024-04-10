using UnityEngine;

[CreateAssetMenu(fileName ="AudioSO", menuName ="DataSO/AudioSO")]
public class AudioSO : ScriptableObject
{
    public AudioClip audioClip;
    public bool loop;
}