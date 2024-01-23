using UnityEngine;

public class LevelManager : SingletonMonoBehaviour<LevelManager>
{
    public int Score { get; private set; }
    public float SessionTime { get; private set; }

    [Header("Level Duration in minutes")]
    public float levelDuration;
    private ScoreCounterUI scoreCounterUI;

    private bool timeOver = false;

    private void Start()
    {
        scoreCounterUI = FindObjectOfType<ScoreCounterUI>();
        TimeManager.ResumeTime();
    }

    private void Update()
    {
        SessionTime += Time.deltaTime;
        if(SessionTime >= levelDuration * 60 && !timeOver)
        {
            timeOver = true;
            //EnemyWaveManager.Instance.ShouldSpawn(false);
            EventManager.OnLevelCompleted?.Invoke();
        }
    }

    public void AddScore()
    {
        Score++;
        scoreCounterUI.SetScore(Score);
    }
}
