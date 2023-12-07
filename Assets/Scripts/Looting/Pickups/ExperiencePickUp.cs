using UnityEngine;

public class ExperiencePickUp : PickUpItem
{
    [SerializeField] private SO_ExperiencePickUpParameters pickupParameters;

    protected override void Collect()
    {
        base.Collect();

        AudioManager.Instance.PlaySound(AudioClipID.ExpPickUp);
        player.GetComponent<ExperienceController>().AddExperience(pickupParameters.expAmount);
        ObjectPooler.Instance.DespawnObject(gameObject);
    }
}
