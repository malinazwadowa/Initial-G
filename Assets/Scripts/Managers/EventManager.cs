using System;

public class EventManager : SingletonMonoBehaviour<EventManager>
{
    /*
     * Works but it seems to be introducing more issues than benefits, gets rid of coupling for weapon and Weapon data but thats not ap roblem anyway.
    main issue is that it calls to eventmanager instance that does not exist in OnValidate.

    public Action<object> onWeaponDataChangedTest;
    public void WeaponDataChangedTest(object weaponData)
    {
        onWeaponDataChangedTest?.Invoke(weaponData);
    } */
}
