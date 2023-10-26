using UnityEngine;

[CreateAssetMenu(fileName = "newPickUpItemData", menuName = "PickUp Items/Exp Orb/Base Data")] 
public class ExperiencePickUp : PickUpableItem
{
    public float expAmount;
    public override void OnPickUp(GameObject collector)
    {
       //give exp logic 
    }

}
