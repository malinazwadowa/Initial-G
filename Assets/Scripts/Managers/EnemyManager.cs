using UnityEngine;

public class EnemyManager : SingletonMonoBehaviour<EnemyManager>
{
    public EnemyManagerData managerData;
    private EnemyWave currentWaveData;

    private int currentWave = 0;

    private float timer;
    public float spawnDelay = 0.5f;

    //public PoolableObject temp;
    // Start is called before the first frame update
    void Start()
    {
        currentWaveData = managerData.enemyWaves[currentWave];
        //SpawnOnCall(PoolableObject.Test, Vector2.zero);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(currentWaveData != null)
        {
            ManageWave();
        }
        
        
        /*
        if (timer > spawnDelay)
        {
            //currentWave++;
            GameObject newEnemy = ObjectPooler.Instance.SpawnObject(PoolableObject.SunflowerOfDoom, GetRandomSpawnPositionOutsideOfCameraView(), transform.rotation);
            newEnemy.GetComponent<Enemy>().Init();
            timer = 0;
        } */
    }
    public void ManageWave()
    {
        for (int i = 0; i < currentWaveData.enemyNumbers.Length; i++)
        {
            PoolableObject enemyType = currentWaveData.enemyNumbers[i].enemyPrefab;
            int amountToKeepActive = currentWaveData.enemyNumbers[i].amount;

            if (ObjectPooler.Instance.CountOfActiveObjectsOfType(enemyType) < amountToKeepActive)
            {
                GameObject newEnemy = ObjectPooler.Instance.SpawnObject(enemyType, GetRandomSpawnPositionOutsideOfCameraView(), transform.rotation);
                newEnemy.GetComponent<Enemy>().Init();
            }
        }
    }

    public Vector2 GetRandomSpawnPositionOutsideOfCameraView()
    {
        //This should probably be cached somewhere... TBD
        float cameraHeight = Camera.main.orthographicSize;
        float cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float spawnOffset = 2;

        Vector2 spawnPosition = Vector2.zero;
        
        //Sets random side.
        int randomInt = Random.Range(1, 5);
        switch (randomInt)
        {
            case 1:
                spawnPosition =  new Vector2(cameraWidth + spawnOffset, 0);
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
    public void SpawnOnCall(PoolableObject enemyType, Vector2 position)
    {
        GameObject newEnemy = ObjectPooler.Instance.SpawnObject(enemyType, position, transform.rotation);
        newEnemy.GetComponent<Enemy>().Init();
    }
}
/*
 * MonoBehaviour[] scripts = newEnemy.GetComponents<MonoBehaviour>();

            // Iterate through the scripts and find the one that inherits from "Enemy"
            foreach (MonoBehaviour script in scripts)
            {
                if (script != null && typeof(Enemy).IsAssignableFrom(script.GetType()))
                {
                    // Use reflection to find and invoke the "Init2" method if it exists
                    MethodInfo method = script.GetType().GetMethod("Init2");

                    if (method != null)
                    {
                        method.Invoke(script, null);
                        break; // Exit the loop once you've found and called the method.
                    }
                }
            }
 */