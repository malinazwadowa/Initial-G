using UnityEngine;

[CreateAssetMenu(fileName = "newPickUpItemData", menuName = "PickUp Items/Red Apple/Base Data")]
public class RedApple : PickUpableItem
{
    public float restoreAmount;
    public override void PickUp(GameObject collector)
    {
        collector.GetComponent<HealthController>().AddCurrentHealth(restoreAmount);
        //Destroy(gameobject);
    }

}
