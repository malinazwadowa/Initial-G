using System;
using UnityEngine;
using UnityEngine.Events;

public class ExperienceController : MonoBehaviour
{
    private int currentExp;
    private int currentLevel;
    private float maxExpForCurrentLevel;
    private ExpBarUI expBar;

    public void Init(ExpBarUI expBar)
    {
        this.expBar = expBar;
        currentExp = 0;
        currentLevel = 1;
        maxExpForCurrentLevel = 100;
        UpdateExpBar();
    }

    public void AddExperience(int amount)
    {
        currentExp += amount;
        UpdateExpBar();
        if(currentExp >= maxExpForCurrentLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentExp = 0;
        currentLevel++;
        maxExpForCurrentLevel *= 1.5f;
        UpdateExpBar();
        AudioSource levelUpSound = AudioManager.Instance.PlaySound(AudioClipID.LevelUp);
        EventManager.OnPlayerLevelUp?.Invoke(levelUpSound);//Will require ID of sort as argument later for multiple players.
    }

    private void UpdateExpBar()
    {
        if (expBar != null)
        {
            expBar.UpdateExpBar(currentExp, maxExpForCurrentLevel);
        }
    }
}
