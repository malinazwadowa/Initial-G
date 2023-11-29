public class PlayerHealthBarUI : HealthBarUI
{
    void Start()
    {
        healthController = PlayerManager.Instance.GetPlayer().GetComponent<HealthController>();

        if (healthController != null)
        {
            healthController.OnHealthChanged += UpdateHealthBar;
        }

        UpdateHealthBar();
    }

    private void OnEnable()
    {
        if (healthController != null)
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
}
