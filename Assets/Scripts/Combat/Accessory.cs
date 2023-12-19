using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessory : Item
{
    protected CharacterStatsController characterStatsController;

    public virtual void Initalize(CharacterStatsController characterStatsController)
    {
        this.characterStatsController = characterStatsController;
        
    }

    public virtual void ApplyEffect()
    {

    }

}
