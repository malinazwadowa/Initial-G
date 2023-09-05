using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class ObjectPooler : SingletonMonoBehaviour<ObjectPooler>
{
    private void OnValidate()
    {
        for (int i = 0; i < pools.Count; ++i)
        {
            pools[i].name = pools[i].objectType.ToString() +" pool";
        }
    }

    [Serializable]
    public class Pool
    {
        [HideInInspector] public string name;
        //[HideInInspector] public List<GameObject> activeObjects = new List<GameObject>();
        public PoolableObject objectType;
        public GameObject prefab;
        public int size;
        public int activeObjectsCount;
    }
    public List<Pool> pools;
    public Dictionary<PoolableObject, List<GameObject>> poolDictionary;
    
    void Start()
    {
        poolDictionary = new Dictionary<PoolableObject, List<GameObject>>();

        foreach (Pool pool in pools)
        {
            List<GameObject> objectPool = new List<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Add(obj);
            }
            poolDictionary.Add(pool.objectType, objectPool);
        }
    }

    public GameObject SpawnObject(PoolableObject objectType, Vector3 position, Quaternion rotation = default)
    {
        if (!poolDictionary.ContainsKey(objectType))
        {
            Debug.LogWarning($"Pool for {objectType} does't exist.");
            return null;
        }
        GameObject objectSpawned = null;
        foreach (var obj in poolDictionary[objectType])
        {
            if (!obj.activeSelf)
            {
                objectSpawned = obj;
                break;
            }
        }
        if (objectSpawned == null)
        {
            objectSpawned = Instantiate(poolDictionary[objectType][0]);
            poolDictionary[objectType].Add(objectSpawned);
            pools.Find(p => p.objectType == objectType).size++;
            Debug.Log($"Added additional object to the pool: {objectType}. Consider increasing predefined pool size.");
        }

        objectSpawned.SetActive(true);
        objectSpawned.transform.SetPositionAndRotation(position, rotation);

        pools.Find(p => p.objectType == objectType).activeObjectsCount++;

        return objectSpawned;
    }

    public void DeSpawnObject(PoolableObject objectType, GameObject objectToDeSpawn)
    {
        objectToDeSpawn.SetActive(false);
        pools.Find(p => p.objectType == objectType).activeObjectsCount--;
    }
    
    public int CountOfActiveObjectsOfType(PoolableObject objectType)
    {
        return pools.Find(p => p.objectType == objectType).activeObjectsCount;
    }
    //Not Used, but might be used / ehhh probl not. 
    public GameObject GetObjectPrefab(PoolableObject objectType)
    {
        if (!poolDictionary.ContainsKey(objectType))
        {
            Debug.LogWarning($"Pool for {objectType} doesn't exist.");
            return null;
        }

        return gameObject;
    }
    public T GetObjectComponent<T>(PoolableObject objectType) where T : Component
    {
        GameObject prefab = GetObjectPrefab(objectType);
        if (prefab == null)
        {
            return null;
        }

        return prefab.GetComponent<T>();
    }
}