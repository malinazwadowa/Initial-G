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
        MoveProjectile();
        RotateProjectile();
        if (Mathf.Abs(transform.position.x) > 100 || Mathf.Abs(transform.position.y) > 100) { ObjectPooler.Instance.DeSpawnObject(rockProperties.prefab, gameObject); }
    }

    public void MoveProjectile()
    {
        Vector3 newPosition = transform.position + direction.normalized * rockProperties.speed * Time.deltaTime;
        transform.position = newPosition;
    }
    
    public void RotateProjectile()
    {
        //hedgehogsTransform.Rotate(Vector3.back, hedgehogProperties.speed * 3 * Time.delta)
        transform.Rotate(Vector3.forward * rockProperties.speed * 100 * Time.deltaTime);
        /*foreach(Transform child in transform)
        {
            child.Rotate(Vector3.forward * rockProperties.speed * 10 * Time.deltaTime);
        }*/
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
