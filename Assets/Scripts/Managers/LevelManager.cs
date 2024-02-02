using UnityEngine;

public class LevelManager : SingletonMonoBehaviour<LevelManager>
{
    public int Score { get; private set; }
    public float SessionTime { get; private set; }

    //[Header("Level Duration in minutes")]
    public float LevelDuration { get; private set; }
    private ScoreCounterUI scoreCounterUI;

    private bool timeOver = false;

    private SO_GameLevel gameLevelData;

    protected override void Awake()
    {
        base.Awake();
        SetGameLevelData();
        LevelDuration = gameLevelData.duration;
    }

    private void Start()
    {
        
        scoreCounterUI = FindObjectOfType<ScoreCounterUI>();
        TimeManager.ResumeTime();
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
        scoreCounterUI.SetScore(Score);
    }

    public void SetGameLevelData()
    {
        gameLevelData = GameManager.Instance.levelDataController.GetCurrentLevelData();
    }
}
