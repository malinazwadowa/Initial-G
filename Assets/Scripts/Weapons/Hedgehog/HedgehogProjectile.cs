using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgehogProjectile : MonoBehaviour
{
    private WeaponProperties hedgehogProperties;
    private Transform weaponsTransform;
    private float timer;
    public Transform hedgehogsTransform;
    public void Init(WeaponProperties hedgehogProperties, Transform weaponsTransform)
    {
        this.hedgehogProperties = hedgehogProperties;
        this.weaponsTransform = weaponsTransform;

        timer = 0;
        hedgehogsTransform.localPosition = new Vector3(0, hedgehogProperties.radius, 0);
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
        transform.Rotate(Vector3.forward, hedgehogProperties.speed * Time.deltaTime);
        hedgehogsTransform.Rotate(Vector3.back, hedgehogProperties.speed * 3 * Time.deltaTime);
    }
    private void UpdatePosition()
    {
        transform.position = weaponsTransform.position;
    }
}
