using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GearUpgradeColumnUI : MonoBehaviour
{
    public Image itemIcon;
    public Button button;
    
    public TextMeshProUGUI headerText;
    public TextMeshProUGUI panelText;
    public TextMeshProUGUI itemNameText;

    public GameObject panelRows;
    public GameObject panelTextObject;
    
    public GearUpgradeRowUI damageRow;
    public GearUpgradeRowUI cooldownRow;
    public GearUpgradeRowUI speedRow;
    public GearUpgradeRowUI amountRow;
    public GearUpgradeRowUI radiusRow;
    public GearUpgradeRowUI durationRow;
    public GearUpgradeRowUI knockbackRow;


    public GearUpgradeRowUI GetRow(string key)
    {
        return key switch
        {
            "damage" => damageRow,
            "cooldown" => cooldownRow,
            "speed" => speedRow,
            "amount" => amountRow,
            "radius" => radiusRow,
            "duration" => durationRow,
            "knockback" => knockbackRow,
            _ => null,
        };
    }


    //public GearUpgradeRowUI GetRow(string key)
    //{
    //    Debug.Log("prsza mnie o row, daje row pis jol");

    //    GearUpgradeRowUI row = null;

    //    switch (key)
    //    {
    //        case "damage":
    //            row = damageRow;
    //            Debug.Log("Returned damageRow");
    //            break;
    //        case "cooldown":
    //            row = cooldownRow;
    //            Debug.Log("Returned cooldownRow");
    //            break;
    //        case "speed":
    //            row = speedRow;
    //            Debug.Log("Returned speedRow");
    //            break;
    //        case "amount":
    //            row = amountRow;
    //            Debug.Log("Returned amountRow");
    //            break;
    //        case "radius":
    //            row = radiusRow;
    //            Debug.Log("Returned radiusRow");
    //            break;
    //        case "duration":
    //            row = durationRow;
    //            Debug.Log("Returned durationRow");
    //            break;
    //        case "knockbackPower":
    //            row = knockbackRow;
    //            Debug.Log("Returned knockbackRow");
    //            break;
    //        default:
    //            Debug.LogError("Invalid key provided: " + key);
    //            break;
    //    }

    //    Debug.Log("Returned row for key '" + key + "': " + (row != null ? row.name : "null"));
    //    return row;
    //}


}
