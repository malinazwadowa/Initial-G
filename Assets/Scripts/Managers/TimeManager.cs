using UnityEngine;

public static class TimeManager
{
    private static float lastTimeScale;
    private static bool isPaused = false;

    public static void PauseTime()
    {
        if(isPaused) { return; }
        lastTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        isPaused = true;
    }
    
    public static void ResumeTime()
    {
        if(!isPaused) { return; }
        Time.timeScale = lastTimeScale;
        isPaused = false;
    }

    public static void SetTimeScale(float timeScaleValue)
    {
        lastTimeScale = Time.timeScale;
        Time.timeScale = timeScaleValue;
    }

    public static void ResetTimeScale()
    {
        Time.timeScale = 1f;
    }
}
