using UnityEngine;
//maybe singleton too ? 
public class TreasureSpawner : MonoBehaviour
{
    public TreasureSpawnerData treasureSpawnerData;
    private float timer;
    private float randomTimeOffset = 0;
    
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > treasureSpawnerData.spawnTimeInterval + randomTimeOffset)
        {
            SpawnTreasure();
            SetRandomTimeOffset();
            timer = 0;
        }
    }

    private void SetRandomTimeOffset()
    {
        randomTimeOffset = Random.Range(-treasureSpawnerData.spawnTimeWindowSize / 2, treasureSpawnerData.spawnTimeWindowSize / 2);
    }

    private void SpawnTreasure()
    {
        Vector3 spawnPosition = Utilities.GetRandomPositionOutsideOfCameraView(-3);
        GameObject treasureContainerToSpawn = Utilities.GetRandomOutOfCollection(treasureSpawnerData.treasureContainers).prefab;
        ObjectPooler.Instance.SpawnObject(treasureContainerToSpawn, spawnPosition);
    }
}
