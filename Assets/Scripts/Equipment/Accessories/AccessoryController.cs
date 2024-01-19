using System.Collections.Generic;
using UnityEngine;

public class AccessoryController : MonoBehaviour
{
    private CharacterStatsController characterStatsController;
    [HideInInspector] public List<Accessory> EquippedAccessories { get; private set; } = new List<Accessory>();


    public void Initialize(CharacterStatsController characterStatsController)
    {
        this.characterStatsController = characterStatsController;
    }

    public void EquipAccessory(Accessory accessory)
    {
        accessory.Initialize(characterStatsController);
        EquippedAccessories.Add(accessory);
    }
}
