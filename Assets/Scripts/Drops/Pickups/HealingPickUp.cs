using UnityEngine;

[CreateAssetMenu(fileName = "newPickUpItemData", menuName = "PickUp Items/Red Apple/Base Data")]
public class HealingPickUp : PickUpableItem
{
    public float restoreAmount;
    public override void OnPickUp(GameObject collector)
    {
        collector.GetComponent<HealthController>().AddCurrentHealth(restoreAmount);
        //Destroy(gameobject);
    }

}
