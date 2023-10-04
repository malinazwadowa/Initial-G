using UnityEngine;

[CreateAssetMenu(fileName = "newPickUpItemData", menuName = "PickUp Items/Exp Orb/Base Data")] 
public class ExperienceOrb : PickUpItem
{
    public float expAmount;
    public override void PickUp(GameObject collector)
    {
       //give exp logic 
    }

}
