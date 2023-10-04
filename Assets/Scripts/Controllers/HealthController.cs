using UnityEngine;

public class HealthController : MonoBehaviour
{
    private float currentHealth;
    private float maxHealth;
    
    public void Init(float maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
    }
    public void SubstractCurrentHealth(float amount)
    {
        currentHealth -= amount;
    }
    public void AddCurrentHealth(float amount)
    {
        if(currentHealth < maxHealth)
        {
            currentHealth += amount;
        }
    }
    public void AddMaxHealth(float amount)
    {
        maxHealth += amount;
    }
    public float CurrentHealth()
    {
        return currentHealth;
    }
    public float CurrentMaxHealth()
    {
        return maxHealth;
    }

}
