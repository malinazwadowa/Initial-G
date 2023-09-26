using System;
using System.Collections;
using System.Collections.Generic;
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
        //Temp out of bounds check, aiming to replace with OutofboundsCOntroller disabling all objects that get off the camera view
        if (Mathf.Abs(transform.position.x) > 100 || Mathf.Abs(transform.position.y) > 100)
        {
            //gameObject.SetActive(false);
            //Debug.Log("despawnuje");
            ObjectPooler.Instance.DeSpawnObject(spearProperties.prefab, gameObject);
        }
    }

    public void MoveProjectile()
    {
        Vector3 newPosition = transform.position + movementDirection * spearProperties.speed * Time.deltaTime;
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
    