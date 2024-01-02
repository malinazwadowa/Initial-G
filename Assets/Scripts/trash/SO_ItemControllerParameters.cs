using System;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "newItemUnlockConditions", menuName = "ScriptableObjects/ItemUnlockConditions")]
public class SO_ItemControllerParameters : ScriptableObject
{
    public bool Initialize;

    private void OnValidate()
    {
        //Weapons
        foreach (WeaponType item in Enum.GetValues(typeof(WeaponType)))
        {
            if (!weaponsConditions.Exists(condition => condition.type == item))
            {
                if(item == WeaponType.DefaultValue) { continue; }
                weaponsConditions.Add(new WepCondition { type = item });
            }
        }
        
        for (int i = 0; i < weaponsConditions.Count; ++i)
        {
            weaponsConditions[i].name = weaponsConditions[i].type.ToString() + " unlock condition";
            weaponsConditions[i].Validate();
        }

        //Accessories
        foreach (AccessoryType item in Enum.GetValues(typeof(AccessoryType)))
        {
            if (!accessoriesConditions.Exists(condition => condition.type == item))
            {
                accessoriesConditions.Add(new AccCondition { type = item });
            }
        }

        for (int i = 0; i < accessoriesConditions.Count; ++i)
        {
            accessoriesConditions[i].name = accessoriesConditions[i].type.ToString() + " unlock condition";
            accessoriesConditions[i].Validate();
        }
    }

    public UnlockCondition GetWeaponCondition(WeaponType type)
    {
        Debug.Log("daje kondyszyn dla " + type.ToString());
        WepCondition weaponCondition = weaponsConditions.Find(condition => condition.type == type);
        if (weaponCondition != null)
        {
            Debug.Log("zwracam kondyszyn");
            return weaponCondition;
        }
        else
        {
            Debug.Log($"Failed to get unlock condition for {type}");
            return null;
        }
    }
    public UnlockCondition GetAccessoryCondition(AccessoryType type)
    {
        AccCondition accessoryCondition = accessoriesConditions.Find(condition => condition.type == type);
        if (accessoryCondition != null)
        {
            return accessoryCondition;
        }
        else
        {
            Debug.Log($"Failed to get unlock condition for {type}");
            return null;
        }
    }

    public List<WepCondition> weaponsConditions = new List<WepCondition>();
    public List<AccCondition> accessoriesConditions = new List<AccCondition>();

    [Serializable] public class WepCondition : UnlockCondition
    {
        [AllowNesting][ReadOnly] public WeaponType type;
    }

    [Serializable] public class AccCondition : UnlockCondition
    {
        [AllowNesting][ReadOnly] public AccessoryType type;
    }

    
}


