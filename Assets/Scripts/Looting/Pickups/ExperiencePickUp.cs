using UnityEngine;

[CreateAssetMenu(fileName = "newPickUpItemParameters", menuName = "ScriptableObjects/Loot/PickUp Items/Exp PickUp Parameters")] 
public class ExperiencePickUp : PickUpableItem
{
    public int expAmount;
    public override void OnPickUp(GameObject collector)
    {
        AudioManager.Instance.PlaySound(AudioClipID.ExpPickUp);
        collector.GetComponent<ExperienceController>().AddExperience(expAmount);
    }

}
