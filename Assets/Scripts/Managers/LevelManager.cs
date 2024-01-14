using UnityEngine;

public class LevelManager : SingletonMonoBehaviour<LevelManager>
{
    public int Score { get; private set; }
    public float SessionTime { get; private set; }
    ScoreCounterUI scoreCounterUI;
    private void Start()
    {
        scoreCounterUI = FindObjectOfType<ScoreCounterUI>();
        Debug.Log("resuming time");
        TimeManager.ResumeTime();

    }

    private void Update()
    {
        SessionTime += Time.deltaTime;
    }

    public void AddScore()
    {
        Score++;
        scoreCounterUI.SetScore(Score);
    }
}
