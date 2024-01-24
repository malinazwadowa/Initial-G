using System;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public event Action OnGamePaused;

    [HideInInspector] public SceneName CurrentScene { get; private set; }
    [HideInInspector] public int CurrentSaveProfileId { get; private set; }
    [HideInInspector] public LevelUnlockController LevelUnlockController { get; private set; }
    [HideInInspector] public GameStatsController gameStatsController;
    [HideInInspector] public ItemDataController itemDataController;
    [HideInInspector] public ProfileController profileController;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        LevelUnlockController = GetComponent<LevelUnlockController>();
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
        gameStatsController.Initalize();
        itemDataController.Initalize();
        SaveSystem.Load();
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
        SaveSystem.Save();
    }

    public void ChangeScene(SceneName sceneName)
    {
        OnSceneChange();
        CurrentScene = sceneName;
        SceneLoadingManager.Instance.Load(sceneName);
    }

    public void ReloadCurrentScene()
    {
        ChangeScene(CurrentScene);
    }
}
