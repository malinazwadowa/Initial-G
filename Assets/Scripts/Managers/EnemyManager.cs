using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SingletonMonoBehaviour<EnemyManager>
{
    public EnemyManagerData enemyManagerData;
    private EnemyWave currentWaveData;

    private int currentWave = 0;
    
    private float timer;

    private float waveDuration;

    
    void Start()
    {
        waveDuration = (enemyManagerData.levelDurationInMinutes / enemyManagerData.enemyWaves.Length) * 60;
        currentWaveData = enemyManagerData.enemyWaves[currentWave];

        foreach (KeyValuePair<GameObject, int> entry in GetAmountOfEachEnemyType())
        {
            ObjectPooler.Instance.CreatePool(entry.Key, entry.Value);
        }

        //Debug.Log("Waveduration is: " + waveDuration +" seconds");
        //SpawnOnCall(PoolableObject.Test, Vector2.zero);
    }

    void Update()
    {
        timer += Time.deltaTime;
        
        if(currentWaveData != null)
        {
            ManageWave();
        }
        
        if(timer >= waveDuration && currentWave < enemyManagerData.enemyWaves.Length - 1)
        {
            currentWave++;
            currentWaveData = enemyManagerData.enemyWaves[currentWave];
            timer = 0;
        }
    }
    public void ManageWave()
    {
        for (int i = 0; i < currentWaveData.enemyNumbers.Length; i++)
        {
            GameObject enemyType = currentWaveData.enemyNumbers[i].enemyPrefab;
            int amountToKeepActive = currentWaveData.enemyNumbers[i].amount;

            if (ObjectPooler.Instance.CountOfActiveObjectsOfType(enemyType) < amountToKeepActive)
            {
                GameObject newEnemy = ObjectPooler.Instance.SpawnObject(enemyType, GetRandomSpawnPositionOutsideOfCameraView(), transform.rotation);
                newEnemy.GetComponent<Enemy>().Init();
            }
        }
    }
    public Dictionary<GameObject,int> GetAmountOfEachEnemyType()
    {
        Dictionary<GameObject, int> enemiesToSpawn = new Dictionary<GameObject, int>();

        for (int i = 0; i < enemyManagerData.enemyWaves.Length; i++)
        {
            for (int j = 0; j < enemyManagerData.enemyWaves[i].enemyNumbers.Length; j++)
            {
                GameObject enemyType = enemyManagerData.enemyWaves[i].enemyNumbers[j].enemyPrefab;
                int amountToSpawn = enemyManagerData.enemyWaves[i].enemyNumbers[j].amount;

                if (!enemiesToSpawn.ContainsKey(enemyType))
                {
                    enemiesToSpawn.Add(enemyType, amountToSpawn);
                }
                else
                {
                    if (enemiesToSpawn[enemyType] < amountToSpawn)
                    {
                        enemiesToSpawn[enemyType] = amountToSpawn;
                    }
                }
            }

        }
        return enemiesToSpawn;
    }
    public Vector2 GetRandomSpawnPositionOutsideOfCameraView()
    {
        float cameraHeight = Camera.main.orthographicSize;
        float cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float spawnOffset = 2;

        Vector2 spawnPosition = Vector2.zero;
        
        //Sets random side.
        int randomInt = Random.Range(1, 5);
        switch (randomInt)
        {
            case 1:
                spawnPosition = new Vector2(cameraWidth + spawnOffset, 0);
                break;
            case 2:
                spawnPosition = new Vector2(-cameraWidth - spawnOffset, 0);
                break;
            case 3:
                spawnPosition = new Vector2(0, cameraHeight + spawnOffset);
                break;
            case 4:
                spawnPosition = new Vector2(0, -cameraHeight - spawnOffset);
                break;
            default:
                Debug.Log("Failed to choose side to spawn");
                break;
        }

        //Sets random position on chosen side.
        if (spawnPosition.x == 0)
        {
            spawnPosition.x = Random.Range((-cameraWidth - spawnOffset), (cameraWidth + spawnOffset));
        }
        else
        {
            spawnPosition.y = Random.Range((-cameraHeight - spawnOffset), (cameraHeight + spawnOffset));
        }
        return spawnPosition;
    }
    public void SpawnOnCall(GameObject enemyType, Vector2 position)
    {
        GameObject newEnemy = ObjectPooler.Instance.SpawnObject(enemyType, position, transform.rotation);
        newEnemy.GetComponent<Enemy>().Init();
    }
}