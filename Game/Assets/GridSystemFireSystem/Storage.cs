using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public static Storage Instance;

    public List<GameObject> StorageList = new List<GameObject>();
    private Dictionary<string, GameObject> ObjectPool = new Dictionary<string, GameObject>();

    void Awake()
    {
        Instance = this;
        InitPool();
    }

    void InitPool()
    {
        foreach(GameObject obj in StorageList)
        {
            ObjectPool.Add(obj.name, obj);
        }

        Debug.Log("Pool count:" + ObjectPool.Count);
        
    }

    public GameObject GetObject(string objectName)
    {
        if(ObjectPool.ContainsKey(objectName))
        {
            return ObjectPool[objectName];
        }
        else
        {
            Debug.Log("Object:" + objectName + "not found");
            return null;
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
