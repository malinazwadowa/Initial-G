using System;
using UnityEngine;
using UnityEngine.Events;

public class ExperienceController : MonoBehaviour
{
    private int currentExp;
    public int CurrentLevel { get; private set; }
    private float maxExpForCurrentLevel;
    private ExpBarUI expBar;

    [SerializeField] private AudioClipNameSelector levelUpSound;

    public void Initialize(ExpBarUI expBar)
    {
        this.expBar = expBar;
        currentExp = 0;
        CurrentLevel = 1;
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
        CurrentLevel++;
        maxExpForCurrentLevel = (float)(maxExpForCurrentLevel * 1.05) + 75;
        UpdateExpBar();
        AudioSource levelUpSoundSource = AudioManager.Instance.PlaySound(levelUpSound.clipName);
        EventManager.OnPlayerLevelUp?.Invoke(levelUpSoundSource);//Will require ID of sort as argument later for multiple players.
    }

    private void UpdateExpBar()
    {
        if (expBar != null)
        {
            expBar.UpdateExpBar(currentExp, maxExpForCurrentLevel);
        }
    }
}
