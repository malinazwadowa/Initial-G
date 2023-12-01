using Newtonsoft.Json;
using System;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public event Action OnGamePaused;
    public SaveData loadedData;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        SaveSystem.Initialize();
        Load();
    }

    private void Start()
    {
        AudioManager.Instance.UpdateSettings();
    }

    public void PauseGame()
    {
        OnGamePaused?.Invoke();
        TimeManager.PauseTime();
    }

    public void StopGame()
    {
        //Logic to freeze enemies
        TimeManager.PauseTime();
    }

    public void ResumeGame()
    {
        TimeManager.ResumeTime();
    }

    public void Save()
    {
        SaveData saveObject = new SaveData();
        
        SaveSystem.Save(saveObject);
    }

    public void Load()
    {
        loadedData = SaveSystem.Load();
    }
}
