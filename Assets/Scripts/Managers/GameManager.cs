using System;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using static GameStatsController;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public event Action OnGamePaused;
    
    [HideInInspector] public GameLevel CurrentGameLevel { get; private set; }
    [HideInInspector] public LevelUnlockController levelUnlockController { get; private set; }
    [HideInInspector] public GameStatsController gameStatsController;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            //Test2();
            SaveSystem.Load();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            //Test3();
            SaveSystem.Save();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Dictionary<GameLevel, bool> levelUnlockStatus = levelUnlockController.GetCurrentLevelUnlockStatus();
            Debug.Log(levelUnlockStatus.Count);
            Debug.Log(levelUnlockStatus[GameLevel.Cementary]);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        levelUnlockController = GetComponent<LevelUnlockController>();
        gameStatsController = GetComponent<GameStatsController>();
        SaveSystem.Initialize();
    }

    private void Start()
    {
        SaveSystem.Load();
        Debug.Log(gameStatsController.gameStats.enemyKilledCounts.TryGetValue(EnemyType.Sunflower, out int currentCount) + " "+ currentCount);
    }

    public void PauseGame()
    {
        OnGamePaused?.Invoke();
        TimeManager.PauseTime();
    }

    public void StopGame()
    {
        TimeManager.PauseTime();
    }

    public void ResumeGame()
    {
        TimeManager.ResumeTime();
    }

    public void LoadGameLevel(GameLevel gameLevel)
    {
        CurrentGameLevel = gameLevel;
        SceneName sceneName = (SceneName)gameLevel;
        SceneLoadingManager.Instance.Load(sceneName);

        gameStatsController.Initalize();
    }
}
