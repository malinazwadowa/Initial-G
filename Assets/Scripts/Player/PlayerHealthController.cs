using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
