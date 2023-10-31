using UnityEngine;

public class HealthController : MonoBehaviour
{
    private float currentHealth;
    private float maxHealth;

    private HealthBarUI healthBar;

    public void Init(float maxHealth, HealthBarUI healthBar = null)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
        this.healthBar = healthBar;
    }
    public void SubstractCurrentHealth(float amount)
    {
        currentHealth -= amount;
        UpdateHealthBar();
    }
    public void AddCurrentHealth(float amount)
    {
        if(currentHealth < maxHealth)
        {
            currentHealth += amount;
        }
        UpdateHealthBar();
    }
    public void AddMaxHealth(float amount)
    {
        maxHealth += amount;
        UpdateHealthBar();
    }
    public float CurrentHealth()
    {
        return currentHealth;
    }
    public float CurrentMaxHealth()
    {
        return maxHealth;
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }
    }
}
