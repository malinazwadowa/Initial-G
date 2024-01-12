using UnityEngine;

public class LevelManager : SingletonMonoBehaviour<LevelManager>
{
    public int Score { get; private set; }
    public float SessionTime { get; private set; }

    private void Update()
    {
        SessionTime += Time.deltaTime;
        Debug.Log($"session time is : {SessionTime}");
    }
    public void AddScore()
    {
        Score++;
    }
}
