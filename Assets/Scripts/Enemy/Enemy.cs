using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] public EnemyData enemyData;

    protected Player player;
    protected HealthController healthController;
    //-----------------
    
    

    public virtual void Init()
    {
        player = PlayerManager.Instance.GetPlayer();
        healthController = GetComponent<HealthController>();
        healthController.Init(enemyData.maxHealth);
    }

    public void GetDamaged(float amount)
    {
        //Possibly armor logic. 
        Debug.Log($"GEtting Damaged for: {amount}");
        healthController.SubstractCurrentHealth(amount);
        if(healthController.CurrentHealth() <= 0)
        {
            Kill();
        }
    }
    public void Kill()
    {
        //some other steps
        //Prefab field from enemy data is... not that nice, might need figuring out.
        ObjectPooler.Instance.DeSpawnObject(enemyData.enemyPrefab, gameObject);
    }
}
