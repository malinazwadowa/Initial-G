using System;
using System.Collections;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    private float currentHealth;
    private float maxHealth;
    private bool isRegenerating;
    private float regenerationRate;
    private CharacterStats characterStats;

    public Action OnHealthChanged;

    /// <summary>
    /// Specifying both optional arguments enables the regeneration coroutine.
    /// <paramref name="regenerationRate"/> - The base amount restored each interval.
    /// </summary>
    public void Initialize(float maxHealth, CharacterStats characterStats = null, float regenerationRate = 0)
    {
        this.maxHealth = maxHealth;
        
        currentHealth = maxHealth;

        if (regenerationRate > 0 && characterStats != null)
        {
            this.characterStats = characterStats;
            EnableRegeneration(regenerationRate);
        }
    }

    public void SubstractCurrentHealth(float amount)
    {
        currentHealth -= amount;
        OnHealthChanged?.Invoke();
    }

    public void AddCurrentHealth(float amount)
    {
        if(currentHealth < maxHealth)
        {
            currentHealth += amount;
            if(currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
        OnHealthChanged?.Invoke();
    }

    public void AddMaxHealth(float amount)
    {
        maxHealth += amount;
        OnHealthChanged?.Invoke();
    }
    ////////////////////////////////////////////
    public void EnableRegeneration(float rate)
    {
        if (!isRegenerating)
        {
            Debug.Log("enabling regenNNNNNNNN");
            isRegenerating = true;
            regenerationRate = rate;
            StartCoroutine(RegenerateHealth());
        }
    }

    public void SetRegenerationRate(float newRate)
    {
        regenerationRate = newRate;
    }

    public void DisableRegeneration()
    {
        isRegenerating = false;
        StopCoroutine(RegenerateHealth());
    }

    private IEnumerator RegenerateHealth()
    {
        while (isRegenerating)
        {
            yield return new WaitForSeconds(3f);

            if(((currentHealth + regenerationRate) * characterStats.healthRegenerationModifier) >= maxHealth)
            {
                currentHealth = maxHealth;
            }
            else
            {
                currentHealth += regenerationRate * characterStats.healthRegenerationModifier;
            }
            Debug.Log("regening");
            OnHealthChanged?.Invoke();
        }
    }
    ////////////////////////////////////////////
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetCurrentMaxHealth()
    {
        return maxHealth;
    }

    public float GetCurrentHealthNormalized()
    {
        return currentHealth / maxHealth;
    }
}
