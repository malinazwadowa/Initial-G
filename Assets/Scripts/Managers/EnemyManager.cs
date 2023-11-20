using System.Collections.Generic;
using Unity.VisualScripting;
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

        if (currentWaveData != null)
        {
            ManageWave();
        }
        
        if (timer >= waveDuration && currentWave < enemyManagerData.enemyWaves.Length - 1)
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
                GameObject newEnemy = ObjectPooler.Instance.SpawnObject(enemyType, Utilities.GetRandomPositionOutsideOfCameraView(enemyManagerData.spawnDistanceOffset), transform.rotation);
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
    
    public void SpawnOnCall(GameObject enemyType, Vector2 position)
    {
        GameObject newEnemy = ObjectPooler.Instance.SpawnObject(enemyType, position, transform.rotation);
        newEnemy.GetComponent<Enemy>().Init();
    }
}