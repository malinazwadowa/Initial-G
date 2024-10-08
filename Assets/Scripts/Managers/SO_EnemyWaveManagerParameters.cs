using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyWaveManagerParameters", menuName = "ScriptableObjects/Enemy Wave Manager Parameters")]
public class SO_EnemyWaveManagerParameters : ScriptableObject
{
    private void OnValidate()
    {
        for (int i = 0; i < enemyWaves.Length; ++i)
        {
            enemyWaves[i].name = "Wave " + (i + 1);
        }
    }

    //[Header("Base Settings")]
    //public float levelDurationInMinutes;

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