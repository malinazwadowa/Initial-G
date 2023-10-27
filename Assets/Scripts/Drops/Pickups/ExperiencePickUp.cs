using UnityEngine;

[CreateAssetMenu(fileName = "newPickUpItemData", menuName = "PickUp Items/Exp Pickup/Base Data")] 
public class ExperiencePickUp : PickUpableItem
{
    public int expAmount;
    public override void OnPickUp(GameObject collector)
    {
        collector.GetComponent<ExperienceController>().AddExperience(expAmount);
    }

}
