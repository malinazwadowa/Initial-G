using NaughtyAttributes;
using UnityEngine;

public class HealingPickUp : Collectible
{
    [Expandable]
    [SerializeField] private SO_HealingPickUpParameters pickupParameters;

    protected override void Collect()
    {
        base.Collect();
        Debug.Log($"gralbym teraz np clipname:{pickupParameters.pickUpClip.clipName}");
        AudioManager.Instance.PlaySound(pickupParameters.pickUpClip.clipName);
        player.GetComponent<HealthController>().AddCurrentHealth(pickupParameters.restoreAmount);
        ObjectPooler.Instance.DespawnObject(gameObject);
    }
}
