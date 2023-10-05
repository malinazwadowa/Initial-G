using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : SingletonMonoBehaviour<ObjectPooler>
{
    public ObjectPoolerSettings poolerSettings;
    private ObjectPoolerSettings poolerSettingsCopy;

    private List<Pool> pools;

    public Dictionary<string, ObjectLists> poolDictionary;


    void Start()
    {
        poolDictionary = new Dictionary<string, ObjectLists>();

        if (poolerSettings != null)
        {
            poolerSettingsCopy = Instantiate(poolerSettings);
            pools = poolerSettingsCopy.pools;
            //pools = poolerSettings.pools; 

            foreach (Pool pool in pools)
            {
                FillOutPool(pool);
                string key = pool.objectType.name.EndsWith("(Clone)") ? pool.objectType.name : pool.objectType.name + "(Clone)";
                poolDictionary.Add(key, pool.objectLists);
            }
        }
        else
        {
            pools = new List<Pool>();
        }
    }

    public GameObject SpawnObject(GameObject objectType, Vector3 position, Quaternion rotation = default)
    {
        string key = objectType.name.EndsWith("(Clone)") ? objectType.name : objectType.name + "(Clone)";

        if (!poolDictionary.ContainsKey(key))
        {
            CreatePool(objectType);
            Debug.Log($"Pool for {objectType} created");
        }

        GameObject objToSpawn = null;

        if (poolDictionary[key].inactiveObjects.Count == 0)
        {
            objToSpawn = Instantiate(objectType);
            objToSpawn.SetActive(true);
            poolDictionary[key].activeObjects.Add(objToSpawn);

            pools.Find(p => p.objectType == objectType).size++;
            //Debug.Log($"Added additional object to the pool: {objectType}. Consider increasing predefined pool size.");
        }

        if (poolDictionary[key].inactiveObjects.Count > 0)
        {
            objToSpawn = poolDictionary[key].inactiveObjects[0];
            poolDictionary[key].inactiveObjects.Remove(objToSpawn);
            objToSpawn.SetActive(true);
            poolDictionary[key].activeObjects.Add(objToSpawn);
        }

        pools.Find(p => p.objectType == objectType).activeObjectsCount = poolDictionary[key].activeObjects.Count;
        objToSpawn.transform.SetPositionAndRotation(position, rotation);

        return objToSpawn;
    }

    public void DeSpawnObject(GameObject objectToDeSpawn)
    {
        string key = objectToDeSpawn.name.EndsWith("(Clone)") ? objectToDeSpawn.name : objectToDeSpawn.name + "(Clone)";
        if (!poolDictionary.ContainsKey(key))
        {
            Debug.Log($"Pool for {objectToDeSpawn} does't exist, cannot despawn the object.");
        }

        if (poolDictionary[key].activeObjects.Contains(objectToDeSpawn))
        {
            poolDictionary[key].activeObjects.Remove(objectToDeSpawn);
            objectToDeSpawn.SetActive(false);
            poolDictionary[key].inactiveObjects.Add(objectToDeSpawn);
        }
    }

    public void CreatePool(GameObject objectType, int size = 0)
    {
        Pool pool = new Pool();

        string key = objectType.name.EndsWith("(Clone)") ? objectType.name : objectType.name + "(Clone)";

        pool.objectType = objectType;
        pool.size = size;

        pools.Add(pool);
        poolDictionary.Add(key, pool.objectLists);

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
        string key = objectType.name.EndsWith("(Clone)") ? objectType.name : objectType.name + "(Clone)";

        if (pools.Find(p => p.objectType == objectType) != null)
        {
            return poolDictionary[key].activeObjects.Count;
        }
        else
        {
            return 0;
        }
    }
    public List<GameObject> GetAllActiveObjectsOfType(GameObject objectType)
    {
        string key = objectType.name.EndsWith("(Clone)") ? objectType.name : objectType.name + "(Clone)";
        return poolDictionary[key].activeObjects;
    }
}