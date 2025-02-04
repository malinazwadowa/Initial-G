using UnityEngine;

public class CrystalProjectile : MonoBehaviour
{
    private string weaponType;
    private Vector3 spawnPoint;

    private float damage;
    private float speed;
    private float knockbackPower;
    private float radius;

    private float pulseFactor;

    [SerializeField]
    private float scalingBottomLimit = -0.5f;
    [SerializeField]
    private float pulseStart = 0.2f;
    [SerializeField]
    private float pulseEnd = 0.55f;


    private float cycleProgress;
    private float averagePulseFactor;
    private float rotationSpeed = 50f;

    public void Initialize(string weaponType, float damage, float speed, float knockbackPower, float radius)
    {
        this.weaponType = weaponType;

        this.damage = damage;
        this.speed = speed;
        this.knockbackPower = knockbackPower;
        this.radius = radius;

    }

    private void OnEnable()
    {
        //transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        cycleProgress = 0;
    }

    private void OnDisable()
    {
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
    }

    void Update()
    {
        Expand();
        Rotate();
        Pulse();
    }

    private void Pulse()
    {
        float lifeTime = cycleProgress / radius;
        float pulseWindow = pulseEnd - pulseStart;

        averagePulseFactor = (1 - pulseWindow) + pulseWindow * ((1f + scalingBottomLimit) * 0.5f);

        if (lifeTime < pulseStart || lifeTime > pulseEnd)
        {
            pulseFactor = 1f;
        }
        else
        {
            float pulseProgress = (lifeTime - pulseStart) / pulseWindow;

            float t = pulseProgress < 0.5f ? pulseProgress * 2f : (1f - pulseProgress) * 2f;
            pulseFactor = Mathf.Lerp(1f, scalingBottomLimit, t);
        }
    }

    private void Rotate()
    {
        transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
    }

    private void Expand()
    {
        if (cycleProgress < radius)
        {
            this.gameObject.transform.localScale += speed * pulseFactor * Time.deltaTime * Vector3.one;
            cycleProgress += speed * averagePulseFactor * Time.deltaTime;

            Debug.Log($"fake:{cycleProgress} nie fejk: {gameObject.transform.localScale.x} averagpulsefactot: {averagePulseFactor}");
        }
        else
        {
            ObjectPooler.Instance.DespawnObject(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable target = collision.gameObject.GetComponent<IDamagable>();
        if (target != null)
        {
            target.Damage(damage, weaponType);
            Vector3 targetPos = collision.transform.position;
            target.Knockback(knockbackPower, targetPos - transform.position);
        }
    }
}
