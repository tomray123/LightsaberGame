using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// Class describes the one type objects pool. 
[System.Serializable]
public class Pool
{
    // Pool tag.
    public string tag;
    // Pool objects prefab.
    public GameObject prefab;
    // Start pool size.
    public int size;
    //Pool objects holder.
    [HideInInspector]
    public GameObject parent;
}

public class ObjectFromPool
{
    // Is object available.
    public bool isAvailable;
    // Pool objects prefab.
    public GameObject obj;
}

public class ObjectPooler : MonoBehaviour
{
    // Pools list.
    public List<Pool> pools;
    // Dictionary of tags and objects in pool.
    public Dictionary<string, List<ObjectFromPool>> poolDictionary;

    // Singleton instance.
    public static ObjectPooler Instance;

    private void Awake()
    {
        // Setting the singleton instance to the ObjectPooler.
        Instance = this;
    }

    // Start is called before the first frame update.
    void Start()
    {
        // Initializing poolDictionary.
        poolDictionary = new Dictionary<string, List<ObjectFromPool>>();

        // Create objects and add them to the pool.
        foreach (Pool pool in pools)
        {
            List<ObjectFromPool> objectPool = new List<ObjectFromPool>();

            if (pool.prefab.GetComponent<PooledObject>() == null)
            {
                Debug.LogWarning("Can't pool prefab " + pool.prefab.name + "! It must have PooledObject Component!");
                return;
            }

            pool.parent = new GameObject(pool.tag + " Pool");

            for (int i = 0; i < pool.size; i++)
            {
                ObjectFromPool poolObj = new ObjectFromPool();
                poolObj.isAvailable = true;
                poolObj.obj = Instantiate(pool.prefab);
                // Setting a holder for objects;
                poolObj.obj.transform.parent = pool.parent.transform;
                PooledObject objComponent = poolObj.obj.GetComponent<PooledObject>();
                objComponent.poolTag = pool.tag;
                objComponent.objIdPool = i;
                poolObj.obj.SetActive(false);
                objectPool.Add(poolObj);
            }

            // Add the pool with its tag to the dictionary.
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    // SpawnFromPool activates object from the object pool.
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        // Check for presence of the required tag.
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        // Creating new GameObject to put the spawned object in it. 
        ObjectFromPool objectToSpawn = null;
        // isFreeFound shows whether inactive gameobject is in the pool.
        bool isFreeFound = false;

        // Search for inactive object to spawn from pool.
        for (int i = 0; i < poolDictionary[tag].Count; i++)
        {
            if (poolDictionary[tag][i].obj == null)
            {
                Pool pool = pools.Find(x => x.tag.Contains(tag));

                if (pool != null)
                {
                    GameObject poolObj = poolDictionary[tag][i].obj;
                    poolObj = Instantiate(pool.prefab);
                    poolObj.transform.parent = pool.parent.transform;
                    poolObj.SetActive(false);
                    // Setting poolObj information;
                    PooledObject objComponent = poolObj.GetComponent<PooledObject>();
                    objComponent.poolTag = pool.tag;
                    objComponent.objIdPool = i;
                    poolDictionary[tag][i].obj = poolObj;
                }
            }

            // If the object is available we will use it.
            if (poolDictionary[tag][i].isAvailable)
            {
                // Get object from the pool.
                objectToSpawn = poolDictionary[tag][i];
                isFreeFound = true;
                break;
            }

        }
        // Check for presence of the available object from the pool.
        if (!isFreeFound)
        {       
            // Find pool with required tag.
            Pool pool = pools.Find(x => x.tag.Contains(tag));

            if (pool != null)
            {
                if (pool.parent == null)
                {
                    pool.parent = Instantiate(new GameObject(pool.tag + " Pool"));
                }

                objectToSpawn = new ObjectFromPool();
                objectToSpawn.isAvailable = true;
                // Instantiate the object.
                objectToSpawn.obj = Instantiate(pool.prefab);
                objectToSpawn.obj.transform.parent = pool.parent.transform;
                // Setting poolObj information;
                PooledObject objComponent = objectToSpawn.obj.GetComponent<PooledObject>();
                objComponent.poolTag = pool.tag;
                objComponent.objIdPool = poolDictionary[tag].Count;
                // Adding new pool object to pool.
                poolDictionary[tag].Add(objectToSpawn);
            }
        }

        // Activate the object.
        objectToSpawn.obj.SetActive(true);
        // Set position and rotation of the object.
        objectToSpawn.obj.transform.position = position;
        objectToSpawn.obj.transform.rotation = rotation;

        objectToSpawn.isAvailable = false;

        // Getting the interface of spawned object in order to call OnObjectSpawn() method.
        IPooledObject iPooledObj = objectToSpawn.obj.GetComponent<IPooledObject>();

        // If spawned object contains classes derived from IPooledObject, OnObjectSpawn() is called.
        if (iPooledObj != null)
        {
            iPooledObj.OnObjectSpawn();
        }

        return objectToSpawn.obj;
    }

    // SpawnFromPool activates object from the object pool.
    public GameObject SpawnFromPool(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        // Check for presence of the required prefab.
        Pool searchedPool = pools.Find(x => ReferenceEquals(prefab, x.prefab));
        if (searchedPool == null)
        {
            Debug.LogWarning("Pool with prefab " + prefab + " doesn't exist.");
            return null;
        }

        string poolTag = searchedPool.tag;

        // Creating new GameObject to put the spawned object in it. 
        ObjectFromPool objectToSpawn = null;
        // isFreeFound shows whether inactive gameobject is in the pool.
        bool isFreeFound = false;

        // Search for inactive object to spawn from pool.
        for (int i = 0; i < poolDictionary[poolTag].Count; i++)
        {
            if (poolDictionary[poolTag][i].obj == null)
            {
                GameObject poolObj = poolDictionary[poolTag][i].obj;
                poolObj = Instantiate(searchedPool.prefab);
                poolObj.transform.parent = searchedPool.parent.transform;
                poolObj.SetActive(false);
                // Setting poolObj information;
                PooledObject objComponent = poolObj.GetComponent<PooledObject>();
                objComponent.poolTag = poolTag;
                objComponent.objIdPool = i;
                poolDictionary[poolTag][i].obj = poolObj;
            }

            // If the object is available we will use it.
            if (poolDictionary[poolTag][i].isAvailable)
            {
                // Get object from the pool.
                objectToSpawn = poolDictionary[poolTag][i];
                isFreeFound = true;
                break;
            }

        }
        // Check for presence of the available object from the pool.
        if (!isFreeFound)
        {
            if (searchedPool.parent == null)
            {
                searchedPool.parent = Instantiate(new GameObject(poolTag + " Pool"));
            }

            objectToSpawn = new ObjectFromPool();
            objectToSpawn.isAvailable = true;
            // Instantiate the object.
            objectToSpawn.obj = Instantiate(searchedPool.prefab);
            objectToSpawn.obj.transform.parent = searchedPool.parent.transform;
            // Setting poolObj information;
            PooledObject objComponent = objectToSpawn.obj.GetComponent<PooledObject>();
            objComponent.poolTag = poolTag;
            objComponent.objIdPool = poolDictionary[poolTag].Count;
            // Adding new pool object to pool.
            poolDictionary[poolTag].Add(objectToSpawn);
        }

        // Activate the object.
        objectToSpawn.obj.SetActive(true);
        // Set position and rotation of the object.
        objectToSpawn.obj.transform.position = position;
        objectToSpawn.obj.transform.rotation = rotation;

        objectToSpawn.isAvailable = false;

        // Getting the interface of spawned object in order to call OnObjectSpawn() method.
        IPooledObject iPooledObj = objectToSpawn.obj.GetComponent<IPooledObject>();

        // If spawned object contains classes derived from IPooledObject, OnObjectSpawn() is called.
        if (iPooledObj != null)
        {
            iPooledObj.OnObjectSpawn();
        }

        return objectToSpawn.obj;
    }

    public void ReturnToPool(GameObject returning)
    {
        PooledObject pooledObj = returning.GetComponent<PooledObject>();

        if (pooledObj == null)
        {
            Debug.LogError("Can't add object to pool! The object " + returning.name + " is not from the pool!");
            Destroy(returning);
            return;
        }

        // Getting the interface of spawned object in order to call OnObjectSpawn() method.
        IPooledObject iPooledObj = returning.GetComponent<IPooledObject>();

        // If spawned object contains classes derived from IPooledObject, OnObjectDestroy() is called.
        if (iPooledObj != null)
        {
            iPooledObj.OnObjectDestroy();
        }

        returning.SetActive(false);
        poolDictionary[pooledObj.poolTag][pooledObj.objIdPool].isAvailable = true;
    }
}
