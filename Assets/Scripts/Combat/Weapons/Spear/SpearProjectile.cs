using UnityEngine;

public class SpearProjectile : MonoBehaviour
{
    
    private Vector3 movementDirection;
    
    private WeaponProperties spearProperties; 
    public void Init(Vector3 movementDirection, WeaponProperties spearProperties)
    {
        this.movementDirection = movementDirection;
        this.spearProperties = spearProperties;

        transform.right = movementDirection;
    }

    void Update()
    {
        MoveProjectile();

        if (!Utilities.IsObjectInView(0.2f, transform.position))
        {
            ObjectPooler.Instance.DeSpawnObject(gameObject, gameObject);
        }
    }

    private void MoveProjectile()
    {
        Vector3 newPosition = transform.position + movementDirection * spearProperties.speed * Time.deltaTime;
        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable target = collision.gameObject.GetComponent<IDamagable>();
        if (target != null)
        {
            Vector3 direction = collision.transform.position - transform.position;

            target.GetDamaged(spearProperties.damage);
            
            target.GetKnockbacked(spearProperties.knockbackPower, direction.normalized);
        }
    }
}
