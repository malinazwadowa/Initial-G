using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : SingletonMonoBehaviour<ObjectPooler>
{
    public SO_ObjectPoolerParameters poolerSettings;
    private SO_ObjectPoolerParameters poolerSettingsCopy;

    private List<Pool> pools;

    public Dictionary<string, ObjectLists> poolDictionary;


    void Start()
    {
        poolDictionary = new Dictionary<string, ObjectLists>();

        if (poolerSettings != null)
        {
            poolerSettingsCopy = Instantiate(poolerSettings);
            pools = poolerSettingsCopy.pools; 

            foreach (Pool pool in pools)
            {
                FillOutEmptyPool(pool);
                poolDictionary.Add(pool.objectPrefab.name, pool.objectLists);
            }
        }

        else
        {
            pools = new List<Pool>();
        }
    }

    public GameObject SpawnObject(GameObject gameObject, Vector3 position, Quaternion rotation = default)
    {
        gameObject.name = gameObject.name.EndsWith("(Clone)") ? gameObject.name.Replace("(Clone)", "") : gameObject.name;
        string objectName = gameObject.name;

        if (!poolDictionary.ContainsKey(gameObject.name))
        {
            CreatePool(gameObject);
            Debug.Log($"Pool for {gameObject} created");
        }

        GameObject objToSpawn = null;

        if (poolDictionary[objectName].inactiveObjects.Count == 0)
        {
            objToSpawn = Instantiate(gameObject, ObjectHolder.GetTransform());
            objToSpawn.name = objectName;
            objToSpawn.SetActive(true);

            poolDictionary[objectName].activeObjects.Add(objToSpawn);
            
            foreach (var pool in pools)
            {
                if (pool.objectPrefab == gameObject)
                {
                    pool.size++;
                    break;
                }
            } 
        }

        else if (poolDictionary[objectName].inactiveObjects.Count > 0)
        {
            objToSpawn = poolDictionary[objectName].inactiveObjects[0];
            poolDictionary[objectName].inactiveObjects.Remove(objToSpawn);
            objToSpawn.SetActive(true);
            poolDictionary[objectName].activeObjects.Add(objToSpawn);
        }

        objToSpawn.transform.SetPositionAndRotation(position, rotation);

        return objToSpawn;
    }

    public void DespawnObject(GameObject objectToDeSpawn)
    {
        if (!poolDictionary.ContainsKey(objectToDeSpawn.name))
        {
            Debug.Log($"Pool for {objectToDeSpawn} doesn't exist, cannot despawn the object.");
            return;
        }

        if (poolDictionary[objectToDeSpawn.name].activeObjects.Contains(objectToDeSpawn))
        {
            poolDictionary[objectToDeSpawn.name].activeObjects.Remove(objectToDeSpawn);
            objectToDeSpawn.SetActive(false);
            poolDictionary[objectToDeSpawn.name].inactiveObjects.Add(objectToDeSpawn);
        }
    }

    public void CreatePool(GameObject gameObject, int size = 0)
    {
        gameObject.name = gameObject.name.EndsWith("(Clone)") ? gameObject.name.Replace("(Clone)", "") : gameObject.name;

        if (poolDictionary.ContainsKey(gameObject.name))
        {
            Debug.Log($"Pool for {gameObject.name} already exists.");
            return;
        }

        Pool pool = new Pool();

        pool.objectPrefab = gameObject;
        pool.size = size;

        pools.Add(pool);
        poolDictionary.Add(gameObject.name, pool.objectLists);

        if (size > 0)
        {
            FillOutEmptyPool(pool);
        }
    }

    public void FillOutEmptyPool(Pool pool)
    {
        for (int i = 0; i < pool.size; i++)
        {
            GameObject gameObject = Instantiate(pool.objectPrefab, ObjectHolder.GetTransform());
            gameObject.name = gameObject.name.EndsWith("(Clone)") ? gameObject.name.Replace("(Clone)", "") : gameObject.name;
            gameObject.SetActive(false);
            pool.objectLists.inactiveObjects.Add(gameObject);
        }
    }

    public int CountOfActiveObjectsOfType(GameObject gameObject)
    {
        if (pools.Find(p => p.objectPrefab == gameObject) != null)
        {
            return poolDictionary[gameObject.name].activeObjects.Count;
        }
        else
        {
            return 0;
        }
    }

    public List<GameObject> GetAllActiveObjectsOfType(GameObject gameObject)
    {
        return poolDictionary[gameObject.name].activeObjects;
    }
}