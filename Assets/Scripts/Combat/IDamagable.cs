using UnityEngine;

public interface IDamagable
{
    public void GetDamaged(float amount, ItemType damageSource = ItemType.None);
    public void GetKilled();
    public void GetKnockbacked(float power, Vector3 knockbackDirection);
}
