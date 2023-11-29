using System;
using UnityEngine;

public static class EventManager
{
    public static Action<AudioSource> OnPlayerLevelUp; //Will need arguments for multi
    public static Action OnPauseRequest;
    public static Action OnPlayerDeath;
    public static Action OnEnemyKilled;
    public static Action<int, Vector3> OnEnemyDamaged;
}
