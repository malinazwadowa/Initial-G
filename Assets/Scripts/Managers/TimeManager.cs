using UnityEngine;

public static class TimeManager
{
    private static float lastTimeScale;
    public static bool IsPaused { get; private set; }

    public static void PauseTime()
    {
        //(isPaused) { return; }
        //lastTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        IsPaused = true;
    }
    
    public static void ResumeTime()
    {
        //if(!isPaused) { return; }
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public static void SetTimeScale(float timeScaleValue)
    {
        lastTimeScale = Time.timeScale;
        Time.timeScale = timeScaleValue;
    }

    public static void ResetTimeScale()
    {
        lastTimeScale = Time.timeScale;
        Time.timeScale = 1f;
    }
}
