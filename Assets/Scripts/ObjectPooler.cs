using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class ObjectPooler : SingletonMonoBehaviour<ObjectPooler>
{
    public class PoolLists
    {
        public List<GameObject> activeObjects = new List<GameObject>();
        public List<GameObject> inactiveObjects = new List<GameObject>();
    }

    public ObjectPoolerSettings poolerSettings;
    private ObjectPoolerSettings poolerSettingsCopy;
    
    private List<Pool> pools;

    public Dictionary<GameObject, PoolLists> poolDictionary;



    void Start()
    {
        poolDictionary = new Dictionary<GameObject, PoolLists>();
        poolerSettingsCopy = Instantiate(poolerSettings);
        pools = poolerSettingsCopy.pools;
        //pools = poolerSettings.pools; 
        

        foreach (Pool pool in pools)
        {
            PoolLists poolLists = new PoolLists();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.objectType);
                obj.SetActive(false);
                poolLists.inactiveObjects.Add(obj);
            }
            poolDictionary.Add(pool.objectType, poolLists);
        }
    }

    public GameObject SpawnObject(GameObject objectType, Vector3 position, Quaternion rotation = default)
    {
        if (!poolDictionary.ContainsKey(objectType))
        {
            Debug.LogWarning($"Pool for {objectType} does't exist.");
            return null;
        }

        GameObject objToSpawn = null;

        if(poolDictionary[objectType].inactiveObjects.Count == 0)
        {
            objToSpawn = Instantiate(poolDictionary[objectType].activeObjects[0]);
            poolDictionary[objectType].activeObjects.Add(objToSpawn);
            
            pools.Find(p => p.objectType == objectType).size++;
            Debug.Log($"Added additional object to the pool: {objectType}. Consider increasing predefined pool size.");
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
            Debug.Log($"Pool for {objectType} does't exist.");
        }

        poolDictionary[objectType].activeObjects.Remove(objectToDeSpawn);
        objectToDeSpawn.SetActive(false);
        poolDictionary[objectType].inactiveObjects.Add(objectToDeSpawn);
        pools.Find(p => p.objectType == objectType).activeObjectsCount = poolDictionary[objectType].activeObjects.Count;
    }
    
    public int CountOfActiveObjectsOfType(GameObject objectType)
    {
        return pools.Find(p => p.objectType == objectType).activeObjectsCount = poolDictionary[objectType].activeObjects.Count;
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