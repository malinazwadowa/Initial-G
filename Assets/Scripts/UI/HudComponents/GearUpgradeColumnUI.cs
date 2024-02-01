using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GearUpgradeColumnUI : MonoBehaviour
{
    [HideInInspector] public GearUpgradeControllerUI myController;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Button button;
    
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private TextMeshProUGUI panelText;
    [SerializeField] private TextMeshProUGUI itemNameText;

    [SerializeField] private GameObject panelRows;
    [SerializeField] private GameObject panelTextObject;
    
    [SerializeField] private GearUpgradeRowUI damageRow;
    [SerializeField] private GearUpgradeRowUI cooldownRow;
    [SerializeField] private GearUpgradeRowUI speedRow;
    [SerializeField] private GearUpgradeRowUI amountRow;
    [SerializeField] private GearUpgradeRowUI piercingRow;
    [SerializeField] private GearUpgradeRowUI radiusRow;
    [SerializeField] private GearUpgradeRowUI durationRow;
    [SerializeField] private GearUpgradeRowUI knockbackRow;

    public void SetUpEquipNew(Item randomItem, Player player)
    {
        itemIcon.sprite = randomItem.baseItemParameters.icon;
        panelTextObject.SetActive(true);
        button.onClick.AddListener(() => OnClickEquipItem(randomItem, player));

        headerText.text = new string($"Equip NEW item");
        itemNameText.text = new string($"{randomItem.GetType().Name}");
        panelText.text = randomItem.baseItemParameters.description;

        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)itemNameText.transform);
    }

    public void SetUpRankUp(Item randomItem)
    {
        itemIcon.sprite = randomItem.baseItemParameters.icon;
        button.onClick.AddListener(() => OnClickRankUpItem(randomItem));

        headerText.text = new string($"Rank up item");
        itemNameText.text = new string($"{randomItem.GetType().Name}");

        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)itemNameText.transform);

        if (randomItem is Weapon weapon)
        {
            SetUpWeaponRankUp(weapon);
        }
        else if (randomItem is Accessory accessory)
        {
            SetUpRankUpAccessory(accessory);
        }
    }

    public void SetUpBlank(Player player)
    {
        panelTextObject.SetActive(true);
        itemIcon.transform.parent.gameObject.SetActive(false);
        itemNameText.gameObject.SetActive(false);

        headerText.text = new string($"You are stocked");
        panelTextObject.GetComponent<TextMeshProUGUI>().text = new string($"Get some healing instead.");

        button.onClick.AddListener(() => OnClickHealPlayer(player));
    }

    private void SetUpWeaponRankUp(Weapon weapon)
    {
        panelRows.SetActive(true);

        Dictionary<string, float> currentParams = weapon.GetParameters(weapon.CurrentRank);
        Dictionary<string, float> nextParams = weapon.GetParameters(weapon.CurrentRank + 1);

        foreach (string key in currentParams.Keys)
        {
            GearUpgradeRowUI row = GetRow(key);
            if (row != null)
            {
                float currentValue = currentParams[key];
                float nextValue = nextParams[key];

                string valueText = currentValue != nextValue ? $"{currentValue}  =>  {nextValue}" : currentValue.ToString();

                row.valueText.text = valueText;
                row.gameObject.SetActive(true);
            }
        }
    }

    private void SetUpRankUpAccessory(Accessory accessory)
    {
        panelTextObject.SetActive(true);

        (StatModifier modifiedStat, float value) = accessory.GetParameters(accessory.CurrentRank);

        string type = GetModifierTypeString(modifiedStat);
        string textInsert = new string($"{type}:\n +{value * 100 * (accessory.CurrentRank + 1)}%  =>  +{value * 100 * (accessory.CurrentRank + 2)}%.");
        panelText.text = textInsert;
    }


    private void OnClickEquipItem(Item item, Player player)
    {
        player.ItemController.EquipItem(item.GetType());
        myController.Close();
    }

    private void OnClickHealPlayer(Player player)
    {
        player.GetComponent<HealthController>().AddCurrentHealth(25);
        AudioManager.Instance.PlaySound(AudioClipID.HealthPickup);
        myController.Close();
    }

    private void OnClickRankUpItem(Item item)
    {
        item.RankUp();
        myController.Close();
    }

    public string GetModifierTypeString(StatModifier modifier) =>
    modifier switch
    {
        StatModifier.MoveSpeedModifier => "Move Speed",
        StatModifier.WeaponSpeedModifier => "Weapon Speed",
        StatModifier.DamageModifier => "Damage Done",
        StatModifier.CooldownModifier => "Cooldown reduction",
        StatModifier.LootingRadius => "Looting Radius",
        StatModifier.RegenerationModifier => "Health regeneration",
        _ => "Unknown"
    };

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
            "piercing" => piercingRow,
            "knockback" => knockbackRow,
            _ => null,
        };
    }
}
