using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolController : MonoBehaviour
{
    public List <ObjectPool> objectPools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public static ObjectPoolController Instance { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
             Destroy(gameObject);
        }

        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (ObjectPool pool in objectPools)
        {
            Queue<GameObject> objPool = new Queue<GameObject>();

            for (int i = 0; i < pool.maxPoolSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                objPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.name, objPool);
        }
    }
    private void Start()
    {
    }
    public GameObject SpwanFromPool(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogError("Tag "+tag+" not Exist in Dictionary");
            return null;
        }
        GameObject objectToSpwan = poolDictionary[tag].Dequeue();
        objectToSpwan.SetActive(true);

        IPooledObject pooledObject = objectToSpwan.GetComponent<IPooledObject>();
        if (pooledObject != null)
        {
            //pooledObject.ReturnToPool();
            pooledObject.OnObjectSpwan();
        }

        poolDictionary[tag].Enqueue(objectToSpwan);
        return objectToSpwan;
    }
    public GameObject SpwanFromPool(string tag,Vector3 position,Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogError("Tag " + tag + " not Exist in Dictionary");
            return null;
        }
        GameObject objectToSpwan = poolDictionary[tag].Dequeue();
        objectToSpwan.SetActive(true);
        objectToSpwan.transform.position = position;
        objectToSpwan.transform.rotation = rotation;

        IPooledObject pooledObject = objectToSpwan.GetComponent<IPooledObject>();
        if (pooledObject != null)
        {
            pooledObject.OnObjectSpwan();
        }

        poolDictionary[tag].Enqueue(objectToSpwan);

        return objectToSpwan;
    }
    public void ReturnToPool(string key)
    {
        if (!poolDictionary.ContainsKey(key))
        {
            Debug.LogError("Tag " + key + " not Exist in Dictionary");
            return;
        }
        //var que = poolDictionary[key].ToArray();
        foreach(GameObject gb in poolDictionary[key])
        {
            gb.GetComponent<IPooledObject>().ReturnToPool();
            gb.SetActive(false);
        }
    }

}
[System.Serializable]
public class ObjectPool
{
    public string name;
    public GameObject prefab;
    public int maxPoolSize;

}
