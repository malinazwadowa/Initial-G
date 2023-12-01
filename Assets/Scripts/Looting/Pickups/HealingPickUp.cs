using UnityEngine;

[CreateAssetMenu(fileName = "newPickUpItemParameters", menuName = "ScriptableObjects/Loot/PickUp Items/Healing PickUp Parameters")]
public class HealingPickUp : PickUpableItem
{
    public float restoreAmount;
    public override void OnPickUp(GameObject collector)
    {
        AudioManager.Instance.PlaySound(AudioClipID.HealthPickup);
        collector.GetComponent<HealthController>().AddCurrentHealth(restoreAmount);
        //Destroy(gameobject);
    }

}
