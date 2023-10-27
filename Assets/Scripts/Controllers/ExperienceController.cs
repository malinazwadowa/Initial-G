using UnityEngine;

public class ExperienceController : MonoBehaviour
{
    private int currentExp;
    private int currentLevel;
    private float maxExpForCurrentLevel;

    public void Init()
    {
        currentExp = 0;
        currentLevel = 1;
        maxExpForCurrentLevel = 100;
    }

    public void AddExperience(int amount)
    {
        currentExp += amount;
        Debug.Log($"Got {amount} exp");
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
        Debug.Log($"Leveled up! Current level is:{currentLevel}. Exp needed for next level:{maxExpForCurrentLevel}");
    }
}
