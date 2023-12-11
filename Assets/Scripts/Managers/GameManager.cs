using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public event Action OnGamePaused;
    public SaveDataOld loadedData;
    //private Dictionary<GameLevel, bool> levelUnlockStatus;
    [HideInInspector] public GameLevel CurrentGameLevel { get; private set; }
    [HideInInspector] public LevelUnlockController levelUnlockController { get; private set; }

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
    


    [Serializable]
    private class GameSaveData : SaveData
    {
        public Dictionary<GameLevel, bool> levelUnlockStatus;
        public GameSaveData()
        {
            //levelUnlockStatus = new Dictionary<GameLevel, bool>();
        }
        public void InitializeLevelUnlocks()
        {


            if (levelUnlockStatus != null)
            {
                return;
            }
            else
            {
                levelUnlockStatus = new Dictionary<GameLevel, bool>();

                foreach (GameLevel level in Enum.GetValues(typeof(GameLevel)))
                {
                    levelUnlockStatus.Add(level, false);
                }

                //UnlockGameLevel(GameLevel.Cementary, levelUnlockStatus);
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        levelUnlockController = GetComponent<LevelUnlockController>();
        
        SaveSystem.Initialize();
    }

    private void Start()
    {
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

}
