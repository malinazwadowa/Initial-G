using UnityEngine;

public class RockProjectile : MonoBehaviour
{
    private ItemType myWeaponType = ItemType.Rock;
    private Transform target;
    private Vector3 throwOrigin;
    private Vector3 direction;

    private float damage;
    private float speed;
    private float knockbackPower;

    public void Initialize(Vector3 throwOrigin, Transform target, float damage, float speed, float knockbackPower)
    {
        
        this.throwOrigin = throwOrigin;
        this.target = target;

        this.damage = damage;
        this.speed = speed;
        this.knockbackPower = knockbackPower;

        direction = this.target.position - this.throwOrigin;
    }
    
    void Update()
    {
        MoveProjectile();
        RotateProjectile();
        if (!Utilities.IsObjectInView(0.2f, transform.position))
        {
            ObjectPooler.Instance.DespawnObject(gameObject);
        }
    }
    
    public void MoveProjectile()
    {
        Vector3 newPosition = transform.position + direction.normalized * speed * Time.deltaTime;
        transform.position = newPosition;
    }
    
    public void RotateProjectile()
    {
        transform.Rotate(Vector3.forward * speed * 100 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable target = collision.gameObject.GetComponent<IDamagable>();
        if ( target != null)
        {
            target.GetDamaged(damage, myWeaponType);
            target.GetKnockbacked(knockbackPower, direction.normalized);
        }
    }
}
