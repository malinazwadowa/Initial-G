using NaughtyAttributes;
using UnityEngine;

public class HealingPickUp : Collectible
{
    [Expandable]
    [SerializeField] private SO_HealingPickUpParameters pickupParameters;

    protected override void Collect()
    {
        base.Collect();

        AudioManager.Instance.PlaySound(AudioClipID.HealthPickup);
        player.GetComponent<HealthController>().AddCurrentHealth(pickupParameters.restoreAmount);
        ObjectPooler.Instance.DespawnObject(gameObject);
    }
}
