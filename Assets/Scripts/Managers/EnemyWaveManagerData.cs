using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyWaveManagerData", menuName = "Data/Enemy Wave Manager/Base Data")]
public class EnemyWaveManagerData : ScriptableObject
{
    private void OnValidate()
    {
        for (int i = 0; i < enemyWaves.Length; ++i)
        {
            enemyWaves[i].name = "Wave " + (i + 1);
        }
    }

    [Header("Base Settings")]
    public float spawnDistanceOffset;
    public float levelDurationInMinutes;

    [Header("Wave Settings")]
    public EnemyWave[] enemyWaves;
}
[System.Serializable]
public class EnemyWave
{
    [HideInInspector] public string name;
    public EnemyNumbers[] enemyNumbers;
}
[System.Serializable]
public class EnemyNumbers
{
    [HideInInspector] public string name;
    public GameObject enemyPrefab;
    public int startAmount;
    public int targetAmount;
}