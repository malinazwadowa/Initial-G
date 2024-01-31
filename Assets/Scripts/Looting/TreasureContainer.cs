using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class TreasureContainer : MonoBehaviour, IDamagable
{
    [Expandable]
    public SO_TreasureContainerParameters containerData;
    private HealthController healthController;
    
    void Start()
    {
        healthController = GetComponent<HealthController>();
        healthController.Initialize(containerData.health);
    }

    public void Damage(float amount, string damageSource)
    {
        healthController.SubstractCurrentHealth(amount);
        if (healthController.GetCurrentHealth() <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        DropLoot(containerData.lootTable);
        ObjectPooler.Instance.DespawnObject(gameObject);
    }

    public void Knockback(float power, Vector3 knockbackDirection)
    {
        
    }

    public void DropLoot(List<ObjectWithWeight> lootTable)
    {
        ObjectPooler.Instance.SpawnObject(Utilities.GetRandomOutOfCollection(lootTable).prefab, transform.position);
    }
}
