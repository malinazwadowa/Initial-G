using UnityEngine;

public static class TimeManager
{
    private static float lastTimeScale;
    private static bool isPaused = false;

    public static void PauseTime()
    {
        Debug.Log("trying to pause time");
        //(isPaused) { return; }
        //lastTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        isPaused = true;
    }
    
    public static void ResumeTime()
    {
        Debug.Log("trying to resume time");
        //if(!isPaused) { return; }
        Time.timeScale = 1f;
        isPaused = false;
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
