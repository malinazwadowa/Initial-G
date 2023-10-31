using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider slider;
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        slider.value = currentHealth/maxHealth;
    }
}
