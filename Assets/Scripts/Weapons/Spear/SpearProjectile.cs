using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearProjectile : MonoBehaviour
{
    private float speed;
    private Vector3 movementDirection;
    private GameObject spearProjectile;
    public void Init(float speed, Vector3 movementDirection, GameObject spearProjectile)
    {
        this.speed = speed;
        this.movementDirection = movementDirection;
        this.spearProjectile = spearProjectile;

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
}
    