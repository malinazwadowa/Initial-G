using System;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public event Action OnGamePaused;

    public void PauseGame()
    {
        OnGamePaused?.Invoke();
        TimeManager.Instance.PauseTime();
    }

    public void StopGame()
    {
        //Logic to freeze enemies
        TimeManager.Instance.PauseTime();
    }

    public void ResumeGame()
    {
        TimeManager.Instance.ResumeTime();
    }

}
