using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Image fillImage;
    protected HealthController healthController;

    public void UpdateHealthBar()
    {
        if(fillImage.fillAmount != healthController.GetCurrentHealthNormalized())
        {
            fillImage.fillAmount = healthController.GetCurrentHealthNormalized();
        }
    }
}
