using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : SingletonMonoBehaviour<LootManager>
{
    public GameObject smallExpPickup;
    public GameObject mediumExpPickup;
    public GameObject largeExpPickup;

    public void DropLoot(int tier, Vector3 position)
    {
        if (DetermineIfDropsLoot(tier))
        {
            ObjectPooler.Instance.SpawnObject(GetLoot(tier), position);
        }
    }

    private bool DetermineIfDropsLoot(int tier)
    {
        int chance = Random.Range(1, 11);
        switch (tier)
        {
            case 1:
                if (chance >= 3)
                {
                    return true;
                }
                return false;

            case 2:
                if (chance >= 3)
                {
                    return true;
                }
                return false;

            case 3:
                return true;

            default:
                return false;
        }
    }
    private GameObject GetLoot(int tier)
    {
        switch (tier)
        {
            case 1:
                return smallExpPickup;
            case 2:
                return mediumExpPickup;
            case 3:
                return largeExpPickup;
            default:
                return null;
        }
    }
}
