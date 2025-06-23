using UnityEngine;

/// <summary>
/// Arm's movement should only handle the arms movement and targetting
/// it will just follow the target and shoot at it
/// </summary>
public class ArmsMovement : MonoBehaviour
{
    // [SerializeField, Range(1, 10)] int maxCollision = 5;
    // [SerializeField, Range(1, 10)] int halfRadius = 1;
    // [SerializeField] private LayerMask mask = default;
    [SerializeField] private Transform weaponHead = default;
    [SerializeField] private Transform target = default;

    // private Collider[] hitColliders;

    private void Start()
    {
        // hitColliders = new Collider[maxCollision];
        // mask = LayerMask.GetMask("Combat");
    }

    private void Update()
    {
        // int totalColls = Physics.OverlapBoxNonAlloc(transform.position, Vector3.one * halfRadius,
        //     hitColliders, Quaternion.identity, mask);

        // WeaponRotation(target);

    }

    private void WeaponRotation(Transform target)
    {
        // Vector3 targetDirection = (target.position - weaponHead.position).normalized;
        // float angle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;

        // Vector3 finalRotation = new Vector3(0f, angle, 0f);
        // weaponHead.localRotation = Quaternion.Slerp(weaponHead.rotation, Quaternion.Euler(finalRotation), Time.deltaTime * 5f);
    }


#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        //Draw gizmos here
    }


#endif
} 