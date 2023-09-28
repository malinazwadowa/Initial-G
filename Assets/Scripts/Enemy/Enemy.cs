using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] public EnemyData enemyData;

    protected Player player;
    protected HealthController healthController;
    private bool isKnockedback;

    public virtual void Init()
    {
        player = PlayerManager.Instance.GetPlayer();
        healthController = GetComponent<HealthController>();
        healthController.Init(enemyData.maxHealth);
        isKnockedback = false;
    }

    public void GetDamaged(float amount)
    {
        //Possibly armor logic. 
        healthController.SubstractCurrentHealth(amount);
        if(healthController.CurrentHealth() <= 0)
        {
            GetKilled();
        }
    }

    public void GetKilled()
    {
        //Prefab field from enemy data is... not that nice, might need figuring out.
        ObjectPooler.Instance.DeSpawnObject(enemyData.enemyPrefab, gameObject);
        StopAllCoroutines();
    }

    public void GetKnockbacked(float power, Vector3 knockbackDirection)
    {
        if (!isKnockedback && gameObject.activeSelf)
        {
            knockbackDirection.Normalize();
            StartCoroutine(ApplyKnockback(power, knockbackDirection));
            isKnockedback = true;
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
