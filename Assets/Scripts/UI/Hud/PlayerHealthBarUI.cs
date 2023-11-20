public class PlayerHealthBarUI : HealthBarUI
{
    void Start()
    {
        healthController = PlayerManager.Instance.GetPlayer().GetComponent<HealthController>();
        healthController.OnHealthChanged += UpdateHealthBar;
        UpdateHealthBar();
    }

    private void OnDisable()
    {
        if (healthController != null)
        {
            healthController.OnHealthChanged -= UpdateHealthBar;
        }
    }
}
