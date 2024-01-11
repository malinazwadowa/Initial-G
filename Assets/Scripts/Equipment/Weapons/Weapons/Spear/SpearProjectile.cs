using UnityEngine;

public class SpearProjectile : MonoBehaviour
{
    private string weaponType;
    private Vector3 movementDirection;
    private float damage;
    private float speed;
    private float knockbackPower;

    public void Init(string weaponType, Vector3 movementDirection, float damage, float speed, float knockbackPower)
    {
        this.weaponType = weaponType;
        this.movementDirection = movementDirection;
        this.damage = damage;
        this.speed = speed;
        this.knockbackPower = knockbackPower;

        transform.right = movementDirection;
    }

    void Update()
    {
        MoveProjectile();

        if (!Utilities.IsObjectInView(0.2f, transform.position))
        {
            ObjectPooler.Instance.DespawnObject(gameObject);
        }
    }

    private void MoveProjectile()
    {
        Vector3 newPosition = transform.position + movementDirection * speed * Time.deltaTime;
        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable target = collision.gameObject.GetComponent<IDamagable>();
        if (target != null)
        {
            Vector3 direction = collision.transform.position - transform.position;

            target.GetDamaged(damage, weaponType);
            
            target.GetKnockbacked(knockbackPower, direction.normalized);
        }
    }
}