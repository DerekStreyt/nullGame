using Engine;
using UnityEngine;

public class UnityPoolObject : MonoBehaviour, IPoolObject
{
    public virtual bool IsPushed { get; set; }
    public Transform MyTransform { get; protected set; }

    protected virtual void Awake()
    {
        MyTransform = transform;
    }

    public virtual void SetTransform(Vector3 position, Quaternion rotation)
    {
        MyTransform.position = position;
        MyTransform.rotation = rotation;
    }

    public virtual void Create()
    {
        MyTransform.SetParent(null);
        gameObject.SetActive(true);
 }

    public virtual void AfterCreate()
    {
    }

    public virtual void OnPush()
    {
        gameObject.SetActive(false);
    }

    public virtual void Push()
    {
        UnityPoolManager.Instance.Push(this);
    }

    public void FailedPush()
    {
        Debug.Log("FailedPush");
        Destroy(gameObject);
    }
}
