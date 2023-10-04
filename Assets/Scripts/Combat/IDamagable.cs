using UnityEngine;

public interface IDamagable
{
    public void GetDamaged(float amount);
    public void GetKilled();
    public void GetKnockbacked(float power, Vector3 knockbackDirection);
}
