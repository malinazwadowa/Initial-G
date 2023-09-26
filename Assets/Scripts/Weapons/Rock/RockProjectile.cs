using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockProjectile : MonoBehaviour
{
    private Transform target;
    private Vector3 throwOrigin;
    private Rigidbody2D myRigidbody;
    private Vector3 direction;

    private WeaponProperties rockProperties;

    public void Init(Vector3 throwOrigin, Transform target, WeaponProperties rockProperties)
    {
        
        this.throwOrigin = throwOrigin;
        this.target = target;
        this.rockProperties = rockProperties;

        direction = this.target.position - this.throwOrigin;
    }
    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        MoveRockProjectile();
        if (Mathf.Abs(transform.position.x) > 100 || Mathf.Abs(transform.position.y) > 100) { ObjectPooler.Instance.DeSpawnObject(rockProperties.prefab, gameObject); }
    }
    public void ThrowRockProjectile()
    {
        direction = target.position - throwOrigin;
        myRigidbody.AddForce(direction.normalized * rockProperties.speed);
        Debug.Log("Direction to : " + direction + " target position: "+ target.position + "rzucam z: "+ throwOrigin); 

    }
    public void MoveRockProjectile()
    {
        Vector3 newPosition = transform.position + direction.normalized * rockProperties.speed * Time.deltaTime;
        transform.position = newPosition;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IDamagable>() != null)
        {
            collision.gameObject.GetComponent<IDamagable>().GetDamaged(rockProperties.damage);
        }
    }
}
