using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] public EnemyData enemyData;

    protected Player player;
    protected HealthController healthController;
    public bool isKnockedback;
    private EnemyMovementController enemyMovementController;


    private float pushForce = 5f;
    public float raycastDistance = 1f;
    public LayerMask enemilayer;

    public static event Action OnEnemyKilled;

    public virtual void Init()
    {
        player = PlayerManager.Instance.GetPlayer();
         
        healthController = GetComponent<HealthController>();
        healthController.Init(enemyData.maxHealth);

        enemyMovementController = GetComponent<EnemyMovementController>();
        isKnockedback = false;
    }
    public virtual void Update()
    {
        if (!Utilities.IsObjectInView(1.2f, transform.position))
        {
            ObjectPooler.Instance.DeSpawnObject(gameObject);
        }
    }
  
    public void GetDamaged(float amount)
    {
        //Possibly armor logic. 
        healthController.SubstractCurrentHealth(amount);
        if(healthController.GetCurrentHealth() <= 0)
        {
            GetKilled();
        }
    }

    public void GetKilled()
    {
        ObjectPooler.Instance.DeSpawnObject(gameObject);
        StopAllCoroutines();
        LootManager.Instance.DropLoot(enemyData.tier, transform.position);
        OnEnemyKilled?.Invoke();
    }

    public void GetKnockbacked(float power, Vector3 knockbackDirection)
    {
        if (!isKnockedback && gameObject.activeSelf)
        {
            float powerSpeedCompensation = (enemyData.speed / 50) + 1;
            knockbackDirection.Normalize();
            StartCoroutine(enemyMovementController.ApplyKnockback(power * powerSpeedCompensation, knockbackDirection));
            //isKnockedback = true;
        }
    }

    private IEnumerator ApplyKnockback(float knockbackPower, Vector3 knockbackDirection)
    {

        float timer = 0;
        float knockbackDuration = knockbackPower/16.7f;
        
        while (timer < knockbackDuration)
        {
            float knockbackToApply = knockbackPower * Time.deltaTime;

            transform.position += knockbackDirection * knockbackToApply;
            timer += Time.deltaTime;
            yield return null;
        }
        isKnockedback = false;
    }
}
