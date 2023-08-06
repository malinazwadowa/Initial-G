using UnityEngine;

/// <summary>
/// 
/// Please introduce a generic Health component instead of concrete type health controller like PlayerHealthController or EnemyHealthController.
/// It should be a generic thing.
/// 
/// </summary>
public class PlayerHealthController : MonoBehaviour
{
    public Player player;
    public static PlayerHealthController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        player = GetComponent<Player>();

    }
    public void SubstractHealth(float amount)
    {
        player.playerData.currentHealth -= amount;
    }
    public float CurrentHealth()
    {
        return player.playerData.currentHealth;
    }
}
