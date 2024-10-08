using UnityEngine;

public class SpearProjectile : MonoBehaviour
{
    private string weaponType;
    private Vector3 movementDirection;
    private float damage;
    private float speed;
    private float knockbackPower;
    private int piercing;
    public void Initialize(string weaponType, Vector3 movementDirection, float damage, float speed, float knockbackPower, int piercing)
    {
        this.weaponType = weaponType;
        this.movementDirection = movementDirection;
        this.damage = damage;
        this.speed = speed;
        this.knockbackPower = knockbackPower;
        this.piercing = piercing;

        transform.right = movementDirection;
    }

    void Update()
    {
        Move();

        if (!Utilities.IsObjectInView(0.2f, transform.position))
        {
            ObjectPooler.Instance.DespawnObject(gameObject);
        }
    }

    private void Move()
    {
        Vector3 newPosition = transform.position + movementDirection * speed * Time.deltaTime;
        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable target = collision.gameObject.GetComponent<IDamagable>();
        if (target != null)
        {
            piercing -= 1;

            Vector3 direction = collision.transform.position - transform.position;

            target.Damage(damage, weaponType);
            
            target.Knockback(knockbackPower, direction.normalized);
            
            if (piercing == 0)
            {
                ObjectPooler.Instance.DespawnObject(gameObject);
            }
        }
    }
}
