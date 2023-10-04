using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleItem : MonoBehaviour, IDamagable
{
    public GameObject destructibleItemData;
    private HealthController healthController;
    
    void Start()
    {
        healthController = GetComponent<HealthController>();
        healthController.Init(50);
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
        ItemDropController.Instance.DropApple(transform.position);
        ObjectPooler.Instance.DeSpawnObject(gameObject, gameObject);
    }

    public void GetKnockbacked(float power, Vector3 knockbackDirection)
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
