using System;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public event Action OnGamePaused;
    [HideInInspector] public SceneName CurrentScene { get; private set; }
    [HideInInspector] public GameLevel CurrentGameLevel { get; private set; }

    [HideInInspector] public LevelDataController levelDataController;
    [HideInInspector] public GameStatsController gameStatsController;
    [HideInInspector] public ItemDataController itemDataController;
    [HideInInspector] public ProfileController profileController;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        levelDataController = GetComponent<LevelDataController>();
        gameStatsController = GetComponent<GameStatsController>();
        itemDataController = GetComponent<ItemDataController>();
        profileController = GetComponent<ProfileController>();
        SaveSystem.Initialize();
        
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B)) { SaveSystem.Save(); }
    }

    private void Start()
    {
        gameStatsController.Initialize();
        itemDataController.Initialize();
        SaveSystem.Load();
        levelDataController.UpdateLevelUnlockDictionary();
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

    private void OnSceneChange()
    {
        gameStatsController.StoreSessionStats();
        itemDataController.CheckForNewUnlocks();
        levelDataController.UpdateLevelUnlockDictionary();
        SaveSystem.Save();
    }

    public void ChangeScene(SceneName sceneName)
    {
        OnSceneChange();
        CurrentScene = sceneName;
        SceneLoadingManager.Instance.Load(sceneName);

        //LevelManager.Instance.SetGameLevelData()
    }

    public void ReloadCurrentScene()
    {
        ChangeScene(CurrentScene);
    }

    public void SetCurrentGameLevel(GameLevel gameLevel)
    {
        this.CurrentGameLevel = gameLevel;
    }
}
