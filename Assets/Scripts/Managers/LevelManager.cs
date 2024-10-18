using System.Collections;
using UnityEngine;

public class LevelManager : SingletonMonoBehaviour<LevelManager>
{
    public int Score { get; private set; }
    public float SessionTime { get; private set; }

    public float LevelDuration { get; private set; }

    private ScoreCounterUI scoreCounterUI;
    private TimerControllerUI timerControllerUI;

    private bool timeOver = false;

    private SO_GameLevel gameLevelData;

    protected override void Awake()
    {
        base.Awake();
        StopAllCoroutines();
        SetGameLevelData();
        LevelDuration = gameLevelData.duration;
    }

    private void Start()
    {
        scoreCounterUI = FindObjectOfType<ScoreCounterUI>();
        if (scoreCounterUI == null) Debug.LogError("ScoreCounterUI was not found on scene!");

        timerControllerUI = FindObjectOfType<TimerControllerUI>();
        if (timerControllerUI == null) Debug.LogError("TimeControllerUI was not found on scene!");

        TimeManager.ResumeTime();
        StartCoroutine(UpdateTimerCoroutine());
        
        AudioManager.Instance.Initialize();
        AudioManager.Instance.PlayMusic(gameLevelData.levelMusic.clipName);
    }

    private void Update()
    {
        SessionTime += Time.deltaTime;
        if(SessionTime >= gameLevelData.duration * 60 && !timeOver && !TimeManager.IsPaused)
        {
            timeOver = true;
            EventManager.OnLevelCompleted?.Invoke();
            GameManager.Instance.gameStatsController.RegisterCompletedLevel(gameLevelData.type);
        }
    }

    public void AddScore()
    {
        Score++;

        if (scoreCounterUI != null)
        {
            scoreCounterUI.SetScore(Score);
        }
        else
        {
            Debug.LogError("ScoreCounterUI is missing!");
        }
    }

    public void SetGameLevelData()
    {
        gameLevelData = GameManager.Instance.levelDataController.GetCurrentLevelData();
    }

    private IEnumerator UpdateTimerCoroutine()
    {
        while (true)
        {
            UpdateTimer();
            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateTimer()
    {
        if(timerControllerUI != null)
        {
            timerControllerUI.SetTimerText(SessionTime);
        }
    } 
}
