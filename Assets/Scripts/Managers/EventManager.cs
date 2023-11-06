using System;

public class EventManager : SingletonMonoBehaviour<EventManager>
{
    public event Action OnPlayerLevelUp; //Will need arguments for multi

    public void PlayerLevelUpEvent()
    {
        OnPlayerLevelUp?.Invoke();
    }
}
