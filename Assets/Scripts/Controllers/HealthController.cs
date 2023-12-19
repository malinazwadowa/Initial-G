using System;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    private float currentHealth;
    private float maxHealth;

    public Action OnHealthChanged;

    public void Initialize(float maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
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
