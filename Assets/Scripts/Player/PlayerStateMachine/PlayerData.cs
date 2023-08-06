using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// DATA IN SCRIPTABLE OBJECTS SHOULD NOT BE CHANGED BY ANY MEANS, IT SHOULD HOLD ONLY STATIC UNCHANGEABLE DATA.
/// 
/// 
/// 
/// </summary>

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
