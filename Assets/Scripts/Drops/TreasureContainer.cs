using UnityEngine;

public class TreasureContainer : MonoBehaviour, IDamagable
{
    public TreasureContainerData containerData;
    private HealthController healthController;
    
    void Start()
    {
        healthController = GetComponent<HealthController>();
        healthController.Init(containerData.health);
    }

    public void GetDamaged(float amount)
    {
        healthController.SubstractCurrentHealth(amount);
        if (healthController.CurrentHealth() <= 0)
        {
            GetKilled();
        }
    }

    public void GetKilled()
    {
        DropLoot();
        ObjectPooler.Instance.DeSpawnObject(gameObject);
    }

    public void GetKnockbacked(float power, Vector3 knockbackDirection)
    {
        
    }

    public void DropLoot()
    {
        int totalWeight = 0;
        foreach (var item in containerData.lootableItems)
        {
            totalWeight += item.weight;
        }

        int randomValue = Random.Range(0, totalWeight);

        foreach (var item in containerData.lootableItems)
        {
            if (randomValue < item.weight)
            {
                ObjectPooler.Instance.SpawnObject(item.prefab, transform.position);
                return;
            }

            randomValue -= item.weight;
        }
    }
}
