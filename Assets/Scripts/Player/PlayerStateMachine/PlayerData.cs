using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    

    [Header("Any State")]
    public float maxHealth = 100;
    public float currentHealth = 100;
    public bool attackingAllowed = true;
    public float moveRatio = 1f;

    [Header("Walking State")]
    public float walkSpeed = 0.5f;
    

    [Header("Running State")]
    public float runSpeed = 2.5f;


    [Header("Stealth State")]
    public float stealthSpeed = 1; 
}
