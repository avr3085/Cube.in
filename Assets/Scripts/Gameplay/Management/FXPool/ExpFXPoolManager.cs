using UnityEngine;

public class ExpFXPoolManager : MonoBehaviour
{
    [SerializeField] private FXPoolSO expFX = default;
    [SerializeField, Range(1, 20)] private int poolSize = 10;

    private void OnEnable()
    {
        //
    }

    private void OnDisable()
    {
        expFX.Disable();
    }

    private void Start()
    {
        expFX.PreWarm(poolSize);
        expFX.SetParent(this.transform);
    }
}