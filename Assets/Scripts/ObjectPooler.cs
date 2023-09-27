using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class ObjectPooler : SingletonMonoBehaviour<ObjectPooler>
{
    public ObjectPoolerSettings poolerSettings;
    private ObjectPoolerSettings poolerSettingsCopy;
    
    private List<Pool> pools;

    public Dictionary<GameObject, ObjectLists> poolDictionary;

    void Start()
    {
        poolDictionary = new Dictionary<GameObject, ObjectLists>();

        if(poolerSettings != null)
        {
            poolerSettingsCopy = Instantiate(poolerSettings);
            pools = poolerSettingsCopy.pools;
            //pools = poolerSettings.pools; 

            foreach (Pool pool in pools)
            {
                FillOutPool(pool);
                poolDictionary.Add(pool.objectType, pool.objectLists);
            }
        }
        else
        {
            pools = new List<Pool>();
        }
        

    }
    
    public GameObject SpawnObject(GameObject objectType, Vector3 position, Quaternion rotation = default)
    {
        if (!poolDictionary.ContainsKey(objectType))
        {
            CreatePool(objectType);
            Debug.Log($"Pool for {objectType} created");
        }

        GameObject objToSpawn = null;

        if(poolDictionary[objectType].inactiveObjects.Count == 0)
        {
            objToSpawn = Instantiate(objectType);
            objToSpawn.SetActive(true);
            poolDictionary[objectType].activeObjects.Add(objToSpawn);
            
            pools.Find(p => p.objectType == objectType).size++;
            //Debug.Log($"Added additional object to the pool: {objectType}. Consider increasing predefined pool size.");
        }

        if(poolDictionary[objectType].inactiveObjects.Count > 0)
        {
            objToSpawn = poolDictionary[objectType].inactiveObjects[0];
            poolDictionary[objectType].inactiveObjects.Remove(objToSpawn);
            objToSpawn.SetActive(true);
            poolDictionary[objectType].activeObjects.Add(objToSpawn);
        }

        pools.Find(p => p.objectType == objectType).activeObjectsCount = poolDictionary[objectType].activeObjects.Count;
        objToSpawn.transform.SetPositionAndRotation(position, rotation);

        return objToSpawn;
    }

    public void DeSpawnObject(GameObject objectType, GameObject objectToDeSpawn)
    {
        if (!poolDictionary.ContainsKey(objectType))
        {
            Debug.Log($"Pool for {objectType} does't exist, cannot despawn the object.");
        }

        poolDictionary[objectType].activeObjects.Remove(objectToDeSpawn);
        objectToDeSpawn.SetActive(false);
        poolDictionary[objectType].inactiveObjects.Add(objectToDeSpawn);
        //pools.Find(p => p.objectType == objectType).activeObjectsCount = poolDictionary[objectType].activeObjects.Count;
        //pools.Find(p => p.objectType == objectType).activeObjectsCount = poolDictionary[objectType].activeObjects.Count;
    }

    public void CreatePool(GameObject objectType, int size = 0)
    {
        Pool pool = new Pool();

        pool.objectType = objectType;
        pool.size = size;

        pools.Add(pool);
        poolDictionary.Add(pool.objectType, pool.objectLists);

        if (size > 0)
        {
            FillOutPool(pool);
        }
    }
    public void FillOutPool(Pool pool)
    {
        for (int i = 0; i < pool.size; i++)
        {
            GameObject obj = Instantiate(pool.objectType);
            obj.SetActive(false);
            pool.objectLists.inactiveObjects.Add(obj);
        }
    }

    public int CountOfActiveObjectsOfType(GameObject objectType)
    {
        if(pools.Find(p => p.objectType == objectType) != null)
        {
            return pools.Find(p => p.objectType == objectType).objectLists.activeObjects.Count;
        }
        else
        {
            return 0;
        }
    }
    public List<GameObject> GetAllActiveObjectsOfType(GameObject objectType)
    {
        return pools.Find(p => p.objectType == objectType).objectLists.activeObjects;
    }

    //Not Used, but might be used / ehhh probl not. 
    public GameObject GetObjectPrefab(GameObject objectType)
    {
        if (!poolDictionary.ContainsKey(objectType))
        {
            Debug.LogWarning($"Pool for {objectType.name} doesn't exist.");
            return null;
        }

        return gameObject;
    }

    public T GetObjectComponent<T>(GameObject objectType) where T : Component
    {
        GameObject prefab = GetObjectPrefab(objectType);
        if (prefab == null)
        {
            return null;
        }

        return prefab.GetComponent<T>();
    }
}