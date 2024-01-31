using UnityEngine;

public interface IDamagable
{
    public void Damage(float amount, string damageSource = null);
    public void Kill();
    public void Knockback(float power, Vector3 knockbackDirection);
}
