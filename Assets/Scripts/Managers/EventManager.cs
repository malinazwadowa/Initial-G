using System;

public static class EventManager
{
    public static Action OnPlayerLevelUp; //Will need arguments for multi
    public static Action OnPauseRequest;
    public static Action OnPlayerDeath;
    public static Action OnEnemyKilled;
}
