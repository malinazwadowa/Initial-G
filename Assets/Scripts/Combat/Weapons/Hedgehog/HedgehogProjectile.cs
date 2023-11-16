using Unity.VisualScripting;
using UnityEngine;

public class HedgehogProjectile : MonoBehaviour
{
    [SerializeField] private Transform hedgehogsTransform;
    [SerializeField] private Collider2D hedgehogsCollider;
    
    private Transform weaponsTransform;
    private float timer;

    private float damage;
    private float speed;
    private float knockbackPower;
    private float duration;

    public void Init(Transform weaponsTransform, float damage, float speed, float knockbackPower, float radius, float duration)
    {
        this.damage = damage;
        this.speed = speed;
        this.knockbackPower = knockbackPower;
        this.duration = duration;

        this.weaponsTransform = weaponsTransform;

        timer = 0;
        
        hedgehogsTransform.localPosition = new Vector3(0, radius, 0);
        hedgehogsCollider.offset = new Vector2(0, radius);
    }
    

    void Update()
    {
        timer += Time.deltaTime;
        Spin();
        UpdatePosition();
        if(timer > duration)
        {
            FinishSpinning();
        }
    }

    private void FinishSpinning()
    {
        ObjectPooler.Instance.DeSpawnObject(gameObject);
    }

    private void Spin()
    {
        transform.Rotate(Vector3.forward, -speed * Time.deltaTime);
        hedgehogsTransform.Rotate(Vector3.back, -speed * 3 * Time.deltaTime);
    }

    private void UpdatePosition()
    {
        transform.position = weaponsTransform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable target = collision.gameObject.GetComponent<IDamagable>();
        if (target != null)
        {
            Vector3 direction = collision.transform.position - weaponsTransform.position;

            target.GetKnockbacked(knockbackPower, direction.normalized);
            target.GetDamaged(damage);
        }
    }
}
