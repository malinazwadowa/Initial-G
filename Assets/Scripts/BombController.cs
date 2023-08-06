using UnityEngine;

public class BombController : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public LayerMask enemyLayers;
    public Transform AttackArea;
    private float explosionRange = 3f;
    private float explosionDamage = 100;
    
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
    }
    //Methods with *Animation* in name are used for animation events.
    public void ExplosionAnimationStart()
    {
        spriteRenderer.enabled = false;

    }
    public void ExplosionAnimationEnd()
    {
        animator.enabled = false;
        GameObject.Destroy(gameObject, 3);
    }   
    public void BombIsGrounded()
    {
        animator.SetBool("isOnGround", true);
    }
    public void ExplosionAnimationDmg()
    {
        Explode();
        AudioManager.Instance.PlaySound(AudioManagerClips.Explosion, audioSource);
    }
    //Deals damage to all enemies in range.
    private void Explode()
    {
        Vector3 explostionSource = transform.position + new Vector3(0, 0.53f, 0);
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(explostionSource, explosionRange, enemyLayers);

        foreach (Collider2D enemy in enemiesHit)
        {
            float distanceRatio = Mathf.Clamp(Vector3.Distance(explostionSource, enemy.transform.position) ,0 ,explosionRange - 0.01f) / explosionRange;
            enemy.GetComponent<EnemyCombatController>().GetDamaged(explosionDamage * (1 - distanceRatio));
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 0.53f, 0), explosionRange);
    }
}
