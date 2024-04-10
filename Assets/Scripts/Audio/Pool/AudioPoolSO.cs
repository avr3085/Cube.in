using UnityEngine;

[CreateAssetMenu(fileName = "AudioPool", menuName = "DataSO/Pool/AudioPool")]
public class AudioPoolSO : ComponentPool<SoundEmitter>
{
    [SerializeField] private AudioFactorySO audioFactorySO;
    protected override IFactory<SoundEmitter> Factory 
    { 
        get => audioFactorySO; 
        set
        {
            audioFactorySO = value as AudioFactorySO;
        }
    }
}