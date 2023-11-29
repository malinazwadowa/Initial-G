using UnityEngine;

[CreateAssetMenu(fileName = "newPickUpItemData", menuName = "PickUp Items/Red Apple/Base Data")]
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
