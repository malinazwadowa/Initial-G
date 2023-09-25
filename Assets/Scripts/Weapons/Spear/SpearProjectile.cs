using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearProjectile : MonoBehaviour
{
    private float speed;
    private Vector3 movementDirection;
    private GameObject spearProjectile;
    private SpearProperties spearProperties; 
    public void Init(float speed, Vector3 movementDirection, GameObject spearProjectile, SpearProperties spearProperties)
    {
        this.speed = speed;
        this.movementDirection = movementDirection;
        this.spearProjectile = spearProjectile;


        this.spearProperties = spearProperties;


        transform.right = movementDirection;
    }

    void Update()
    {
        MoveProjectile();
        //Temp out of bounds check, aiming to replace with OutofboundsCOntroller disabling all objects that get off the camera view
        if (Mathf.Abs(transform.position.x) > 100 || Mathf.Abs(transform.position.y) > 100)
        {
            //gameObject.SetActive(false);
            //Debug.Log("despawnuje");
            ObjectPooler.Instance.DeSpawnObject(spearProjectile, gameObject);
        }
    }

    public void MoveProjectile()
    {
        Vector3 newPosition = transform.position + movementDirection * speed * Time.deltaTime;
        //transform.right = movementDirection;
        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Collided with something");
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.GetComponent<IDamagable>() != null)
        {
            //Debug.Log("Target is damagable");
            collision.gameObject.GetComponent<IDamagable>().GetDamaged(spearProperties.damage);
        }
    }
}
    