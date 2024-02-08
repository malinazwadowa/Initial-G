using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SwordProjectile : MonoBehaviour
{
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private float trailSizeModifier;

    private string weaponType;
    private float damage;
    private float knockbackPower;

    private Transform weaponsTransform;
    private bool isRotating = true;
    private float speed = 300;
    private float speedModifier = 1;

    private Vector3 positionOffset;
    private Quaternion targetRotation;

    void Update()
    {
        UpdatePosition();

        if (isRotating)
        {
            float rotationSpeed = speed * Time.deltaTime * speedModifier;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                isRotating = false;
                ObjectPooler.Instance.DespawnObject(gameObject);
            }
        }
    }

    public void Initialize(string weaponType, Transform weaponsTransform, Vector2 facingDirection, float speedModifier, float damage, float radius, float knockbackPower)
    {
        this.weaponType = weaponType;
        this.damage = damage;
        this.knockbackPower = knockbackPower;
        this.weaponsTransform = weaponsTransform;
        this.speedModifier = speedModifier;

        isRotating = true;
        float radiusAdjusted = radius / 5;

        if (facingDirection.x > 0)
        {
            positionOffset = Vector3.right / 5  * radiusAdjusted;
            transform.rotation = Quaternion.Euler(50f, 0f, 80f);
            targetRotation = Quaternion.Euler(20f, 0f, -70f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(50f, 180f, 80f);
            positionOffset = Vector3.left / 5 * radiusAdjusted;
            targetRotation = Quaternion.Euler(20f, 180f, -70f);
        }

        transform.localScale = new Vector3(radiusAdjusted, radiusAdjusted, 1f);
        trailRenderer.widthMultiplier = trailSizeModifier * radius;
    }

    private void UpdatePosition()
    {
        transform.position = weaponsTransform.position + positionOffset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable target = collision.gameObject.GetComponent<IDamagable>();
        if (target != null)
        {
            Vector3 direction = collision.transform.position - weaponsTransform.position;

            target.Knockback(knockbackPower, direction.normalized);
            target.Damage(damage, weaponType);
        }
    }
}
