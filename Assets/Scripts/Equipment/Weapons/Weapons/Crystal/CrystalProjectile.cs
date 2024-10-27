using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalProjectile : MonoBehaviour
{
    private string weaponType;
    private Vector3 spawnPoint;

    private float damage;
    private float speed;
    private float knockbackPower;
    private float duration;


    private float timer;

    public void Initialize(string weaponType, float damage, float speed, float knockbackPower, float duration)
    {
        this.weaponType = weaponType;

        this.damage = damage;
        this.speed = speed;
        this.knockbackPower = knockbackPower;
        this.duration = duration;
        timer = 0;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            ObjectPooler.Instance.DespawnObject(gameObject);
            transform.localScale = Vector3.one;
        }

        Expand();
    }


    private void Expand()
    {
        this.gameObject.transform.localScale += Vector3.one * speed * Time.deltaTime;
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
