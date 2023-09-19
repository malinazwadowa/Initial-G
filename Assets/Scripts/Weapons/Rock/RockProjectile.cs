using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockProjectile : MonoBehaviour
{
    private float speed;
    private Transform target;
    private Vector3 throwOrigin;
    private Rigidbody2D myRigidbody;
    private Vector3 direction;
    private GameObject prefab;

    public void Init(float speed, Vector3 throwOrigin, Transform target, GameObject prefab)
    {
        this.speed = speed;
        this.throwOrigin = throwOrigin;
        this.target = target;
        this.prefab = prefab;

        direction = this.target.position - this.throwOrigin;
    }
    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        MoveRockProjectile();
        if (Mathf.Abs(transform.position.x) > 100 || Mathf.Abs(transform.position.y) > 100) { ObjectPooler.Instance.DeSpawnObject(prefab, gameObject); }
    }
    public void ThrowRockProjectile()
    {
        direction = target.position - throwOrigin;
        myRigidbody.AddForce(direction.normalized * speed);
        Debug.Log("Direction to : " + direction + " target position: "+ target.position + "rzucam z: "+ throwOrigin); 

    }
    public void MoveRockProjectile()
    {
        //Debug.Log("Moving");
        Vector3 newPosition = transform.position + direction.normalized * speed * Time.deltaTime;
        transform.position = newPosition;
    }
}