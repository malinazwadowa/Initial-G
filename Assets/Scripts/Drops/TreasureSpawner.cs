using UnityEngine;

public class TreasureSpawner : MonoBehaviour
{
    public TreasureSpawnerData treasureSpawnerData;
    private float timer;
    private float randomTimeOffset = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

    /*private IEnumerator SpawnTreasures()
{

} */
    private void SpawnTreasure()
    {
        Vector3 spawnPosition = Utilities.GetRandomSpawnPositionOutsideOfCameraView(-3);
        //ObjectPooler.Instance.SpawnObject(treasureSpawnerData.treasures[0], spawnPosition);
        GameObject treasureContainerToSpawn = Utilities.GetRandomOutOfCollection(treasureSpawnerData.treasureContainers).prefab;
        ObjectPooler.Instance.SpawnObject(treasureContainerToSpawn, spawnPosition);
    }
}
