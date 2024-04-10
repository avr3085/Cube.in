using UnityEngine;

public abstract class ComponentPool<T> : PoolSO<T> where T : Component
{
    private Transform poolRoot;

    public Transform PoolRoot
    {
        get
        {
            if(poolRoot == null)
            {
                poolRoot = new GameObject(name).transform;
            }
            return poolRoot;
        }
    }

    private Transform parent;

    public void SetParent(Transform t)
    {
        parent = t;
        poolRoot.SetParent(parent);
    }

    public override T Create()
    {

        T newMember = base.Create();
        newMember.transform.SetParent(PoolRoot);
        newMember.gameObject.SetActive(false);
        return newMember;
    }

    public override T Request()
    {
        T member = base.Request();
        member.gameObject.SetActive(true);
        return member;
    }

    public override void Return(T member)
    {
        member.gameObject.SetActive(false);
        base.Return(member);
    }

    public override void Disable()
    {
        base.Disable();
        #if UNITY_EDITOR
            DestroyImmediate(PoolRoot.gameObject);
        #else
            Destroy(PoolRoot.gameObject);
        #endif
    }
}