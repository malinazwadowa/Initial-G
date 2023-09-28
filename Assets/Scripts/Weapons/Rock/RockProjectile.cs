using UnityEngine;

public class RockProjectile : MonoBehaviour
{
    private Transform target;
    private Vector3 throwOrigin;
    private Vector3 direction;
    private WeaponProperties rockProperties;

    public void Init(Vector3 throwOrigin, Transform target, WeaponProperties rockProperties)
    {
        
        this.throwOrigin = throwOrigin;
        this.target = target;
        this.rockProperties = rockProperties;

        direction = this.target.position - this.throwOrigin;
    }
    
    void Update()
    {
        MoveRockProjectile();
        if (Mathf.Abs(transform.position.x) > 100 || Mathf.Abs(transform.position.y) > 100) { ObjectPooler.Instance.DeSpawnObject(rockProperties.prefab, gameObject); }
    }

    public void MoveRockProjectile()
    {
        Vector3 newPosition = transform.position + direction.normalized * rockProperties.speed * Time.deltaTime;
        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable target = collision.gameObject.GetComponent<IDamagable>();
        if ( target != null)
        {
            target.GetDamaged(rockProperties.damage);
            target.GetKnockbacked(rockProperties.knockbackPower, direction);
        }
    }
}
