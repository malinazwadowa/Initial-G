using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : SingletonMonoBehaviour<EnemyWaveManager>
{
    [Expandable]
    public SO_EnemyWaveManagerParameters enemyManagerData;
    private EnemyWave currentWaveData;

    private int currentWaveId = 0;

    public float spawnOffset = 2.5f;

    private float timer;
    private float lerpFactor;
    private float waveDuration;
    private float ratio = 0.6f; //0-1 ratio for lerp, % of wave time at which peak count of enemies is being kept active

    private bool shouldSpawn = true;

    void Start()
    {
        waveDuration = (enemyManagerData.levelDurationInMinutes / enemyManagerData.enemyWaves.Length) * 60;
        currentWaveData = enemyManagerData.enemyWaves[currentWaveId];

        foreach (KeyValuePair<GameObject, int> entry in GetAmountOfEachEnemyType())
        {
            ObjectPooler.Instance.CreatePool(entry.Key, entry.Value);
        }
    }
    
    void Update()
    {
        timer += Time.deltaTime;

        if (currentWaveData != null && shouldSpawn)
        {
            ManageWave();
        }
        
        if (timer >= waveDuration && currentWaveId < enemyManagerData.enemyWaves.Length - 1)
        {
            currentWaveId++;
            currentWaveData = enemyManagerData.enemyWaves[currentWaveId];
            timer = 0;
        }

        lerpFactor = timer / (waveDuration * ratio);

        if(!shouldSpawn)
        {

        }
    }

    public void ManageWave()
    {
        for (int i = 0; i < currentWaveData.enemyNumbers.Length; i++)
        {
            int amountToKeepActive = (int)Mathf.Lerp(currentWaveData.enemyNumbers[i].startAmount, currentWaveData.enemyNumbers[i].targetAmount, lerpFactor);

            if (ObjectPooler.Instance.CountOfActiveObjectsOfType(currentWaveData.enemyNumbers[i].enemyPrefab) < amountToKeepActive)
            {
                GameObject newEnemy = ObjectPooler.Instance.SpawnObject(currentWaveData.enemyNumbers[i].enemyPrefab, Utilities.GetRandomPositionOutsideOfCameraView(spawnOffset), transform.rotation);
                newEnemy.GetComponent<Enemy>().Initialize();
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
                int amountToSpawn = enemyManagerData.enemyWaves[i].enemyNumbers[j].targetAmount;

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
    
    public void ShouldSpawn(bool status)
    {
        shouldSpawn = status;
    }

    public void SpawnOnCall(GameObject enemyType, Vector2 position)
    {
        GameObject newEnemy = ObjectPooler.Instance.SpawnObject(enemyType, position, transform.rotation);
        newEnemy.GetComponent<Enemy>().Initialize();
    }
}