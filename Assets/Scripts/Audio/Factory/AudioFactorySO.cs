using UnityEngine;

[CreateAssetMenu(fileName = "AudioFactorySO", menuName = "DataSO/Factory/AudioFactory")]
public class AudioFactorySO : FactorySO<SoundEmitter>
{
    [SerializeField] private SoundEmitter prefab = default;
    public override SoundEmitter Create()
    {
        return Instantiate(prefab);
    }
}