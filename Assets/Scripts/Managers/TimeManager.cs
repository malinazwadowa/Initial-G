using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : SingletonMonoBehaviour<TimeManager>
{
    private float lastTimeScale;
    private bool isPaused = false;

    private void Start()
    {
        Time.timeScale = 1.0f;
    }
    public void PauseTime()
    {
        if(isPaused) { return; }
        lastTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        isPaused = true;
    }
    
    public void ResumeTime()
    {
        if(!isPaused) { return; }
        Time.timeScale = lastTimeScale;
        isPaused = false;
    }

    public void SetTimeScale(float timeScaleValue)
    {
        lastTimeScale = Time.timeScale;
        Time.timeScale = timeScaleValue;
    }

    public void ResetTimeScale()
    {
        Time.timeScale = 1f;
    }
}
