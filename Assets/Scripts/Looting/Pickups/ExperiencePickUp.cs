using NaughtyAttributes;
using UnityEngine;

public class ExperiencePickUp : Collectible
{
    [Expandable]
    [SerializeField] private SO_ExperiencePickUpParameters pickupParameters;
    

    protected override void Collect()
    {
        base.Collect();

        AudioManager.Instance.PlaySound(pickupParameters.pickUpClip.clipName);
        player.GetComponent<ExperienceController>().AddExperience(pickupParameters.expAmount);
        ObjectPooler.Instance.DespawnObject(gameObject);
    }
}
