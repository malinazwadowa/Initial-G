using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Image fillImage;

    protected HealthController healthController;

    private void OnEnable()
    {
        if(healthController != null)
        {
            healthController.OnHealthChanged += UpdateHealthBar;
        }
    }
    private void OnDisable()
    {
        if (healthController != null)
        {
            healthController.OnHealthChanged -= UpdateHealthBar;
        }
    }

    public void UpdateHealthBar()
    {
        if(fillImage.fillAmount != healthController.GetCurrentHealthNormalized())
        {
            fillImage.fillAmount = healthController.GetCurrentHealthNormalized();
        }
    }
}
