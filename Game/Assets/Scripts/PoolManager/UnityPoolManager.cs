using Engine;
using UnityEngine;

public class UnityPoolManager : MonoBehaviour
{
    public static UnityPoolManager Instance {get; protected set;}

    public int maxInstanceCount = 2048;

    public PoolManager<UnityPoolObject> Pool { get; protected set; }

    protected Transform parentChildren;

    protected virtual void Awake()
    {
        Instance = this;
        parentChildren = transform;
        Pool = new PoolManager<UnityPoolObject>(maxInstanceCount);
    }

    protected virtual void OnDestroy()
    {
        Instance = null;
    }

    public virtual bool Push(UnityPoolObject poolObject)
    {
        bool result = Pool.Push(poolObject);
        if (result)
        {
            poolObject.MyTransform.SetParent(parentChildren);
        }

        return result;
    }

    public virtual T PopOrCreate<T>(T prefab) where T : UnityPoolObject
    {
        return PopOrCreate(prefab, Vector3.zero, Quaternion.identity);
    }

    public virtual T PopOrCreate<T>(T prefab, Vector3 position, Quaternion rotation) where T : UnityPoolObject
    {      
        T result = Pool.Pop<T>();        
        if (result == null)
        {          
            result = CreateObject(prefab, position, rotation);            
        }
        else
        {
            result.SetTransform(position, rotation);            
        }
        result.AfterCreate();
        return result;
    }

    protected virtual T CreateObject<T>(T prefab, Vector3 position, Quaternion rotation) where T : UnityPoolObject
    {        
        T result = Instantiate(prefab, position, rotation);       
        result.name = prefab.name;
        result.Create();
        return result;
    }
}
