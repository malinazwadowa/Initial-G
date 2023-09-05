using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearProjectile : MonoBehaviour
{
    private float speed;
    private Vector3 movementDirection;
    private PoolableObject spearProjectile;
    public void Init(float speed, Vector3 movementDirection, PoolableObject spearProjectile)
    {
        this.speed = speed;
        this.movementDirection = movementDirection;
        this.spearProjectile = spearProjectile;
    }

    void Update()
    {
        MoveProjectile();
        //Temp out of bounds check, aiming to replace with OutofboundsCOntroller disabling all objects that get off the camera view
        if (Mathf.Abs(transform.position.x) > 100 || Mathf.Abs(transform.position.y) > 100)
        {
            //gameObject.SetActive(false);
            ObjectPooler.Instance.DeSpawnObject(spearProjectile, gameObject);
        }
    }

    public void MoveProjectile()
    {
        Vector3 newPosition = transform.position + movementDirection * speed * Time.deltaTime;
        transform.position = newPosition;
    }
}
    