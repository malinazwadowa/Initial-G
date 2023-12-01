using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class TreasureContainer : MonoBehaviour, IDamagable
{
    public TreasureContainerParameters containerData;
    private HealthController healthController;
    
    void Start()
    {
        healthController = GetComponent<HealthController>();
        healthController.Init(containerData.health);
    }

    public void GetDamaged(float amount)
    {
        healthController.SubstractCurrentHealth(amount);
        if (healthController.GetCurrentHealth() <= 0)
        {
            GetKilled();
        }
    }

    public void GetKilled()
    {
        DropLoot(containerData.lootTable);
        ObjectPooler.Instance.DespawnObject(gameObject);
    }

    public void GetKnockbacked(float power, Vector3 knockbackDirection)
    {
        
    }

    public void DropLoot(List<ObjectWithWeight> lootTable)
    {
        ObjectPooler.Instance.SpawnObject(Utilities.GetRandomOutOfCollection(lootTable).prefab, transform.position);
    }
}
