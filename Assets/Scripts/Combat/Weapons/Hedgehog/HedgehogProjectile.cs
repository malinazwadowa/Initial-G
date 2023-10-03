using UnityEngine;

public class HedgehogProjectile : MonoBehaviour
{
    [SerializeField] private Transform hedgehogsTransform;
    [SerializeField] private Collider2D hedgehogsCollider;
    
    private WeaponProperties hedgehogProperties;
    private Transform weaponsTransform;
    private float timer;

    public void Init(WeaponProperties hedgehogProperties, Transform weaponsTransform)
    {
        this.hedgehogProperties = hedgehogProperties;
        this.weaponsTransform = weaponsTransform;

        timer = 0;
        
        hedgehogsTransform.localPosition = new Vector3(0, hedgehogProperties.radius, 0);
        hedgehogsCollider.offset = new Vector2(0, hedgehogProperties.radius);
    }
    

    void Update()
    {
        timer += Time.deltaTime;
        Spin();
        UpdatePosition();
        if(timer > hedgehogProperties.duration)
        {
            FinishSpinning();
        }
    }

    private void FinishSpinning()
    {
        ObjectPooler.Instance.DeSpawnObject(hedgehogProperties.prefab, gameObject);
    }

    private void Spin()
    {
        transform.Rotate(Vector3.forward, -hedgehogProperties.speed * Time.deltaTime);
        hedgehogsTransform.Rotate(Vector3.back, -hedgehogProperties.speed * 3 * Time.deltaTime);
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

            target.GetKnockbacked(hedgehogProperties.knockbackPower, direction);
            target.GetDamaged(hedgehogProperties.damage);
        }
    }
}
