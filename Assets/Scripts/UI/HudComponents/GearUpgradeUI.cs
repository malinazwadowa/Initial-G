using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GearUpgradeUI : MonoBehaviour
{
    [SerializeField] private GameObject columnPrefab;
    [SerializeField] private Transform content;

    private List<Item> upgradableItems = new List<Item>();
    private List<Item> equippableItems = new List<Item>();

    private Player player;
    private PlayerInputController inputController;
 
    [SerializeField] private MenuUI myMenu;
    private AudioSource levelUpSound;

    private void OnEnable()
    {
        EventManager.OnPlayerLevelUp += Open;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerLevelUp -= Open;
    }

    private void Open(AudioSource levelUpSound)
    {
        this.levelUpSound = levelUpSound;
        TimeManager.PauseTime();
        inputController = PlayerManager.Instance.GetPlayerInputController();
        inputController.SwitchActionMap(inputController.inputActions.PopUpActions);
        player = PlayerManager.Instance.GetPlayer(); //To be replaced by argument either WeaponController or EntirePlayer or at least players ID to provide for GetPlayer()
        myMenu.Open();
    }

    private void Close()
    {
        levelUpSound?.Stop();
        inputController.SwitchActionMap(inputController.inputActions.GameplayActions);
        TimeManager.ResumeTime();
        myMenu.Close();
    }

    private void OnClickRankUpItem(Item item)
    {
        item.RankUp();
        Close();
    }

    private void OnClickEquipItem(Item item)
    {
        player.ItemController.EquipItem(item.GetType());
        Close();
    }

    private void OnClickHealPlayer(Player player)
    {
        player.GetComponent<HealthController>().AddCurrentHealth(25);
        AudioManager.Instance.PlaySound(AudioClipID.HealthPickup);
        Close();
    }

    private void UpdateEquipmentState()
    {
        upgradableItems = player.ItemController.GetUpgradableItems();
        equippableItems = player.ItemController.GetEquippableItems();
    }

    private UpgradeCase GetUpgradeCase()
    {
        UpgradeCase upgradeCase;

        if (equippableItems.Count > 0 && upgradableItems.Count > 0 )
        {
            upgradeCase = UpgradeCase.UpgradeOrNew;
        }
        else if (equippableItems.Count > 0 && upgradableItems.Count == 0 )
        {
            upgradeCase = UpgradeCase.JustNew;
        }
        else if (equippableItems.Count == 0 && upgradableItems.Count > 0)
        {
            upgradeCase = UpgradeCase.JustUpgrade;
        }
        else
        {
            upgradeCase = UpgradeCase.None;
        }

        return upgradeCase;
    }

    public void PresentEquipmentUpgradeChoices()
    {
        UpdateEquipmentState();
        Utilities.RemoveChildren(content);

        List<GameObject> columns = new List<GameObject>();
        int amountOfUpgrades = 2;

        for (int i = 0; i < amountOfUpgrades; i++)
        {
            GameObject column = Instantiate(columnPrefab);
            column.transform.SetParent(content.transform, false);
            UpgradeCase upgradeCase = GetUpgradeCase();
            SetUpOptions(upgradeCase, column);
        }
    }

    private void SetUpOptions(UpgradeCase upgradeCase, GameObject columnObject)
    {
        GearUpgradeColumnUI column = columnObject.GetComponent<GearUpgradeColumnUI>();

        switch (upgradeCase)
        {
            case UpgradeCase.UpgradeOrNew:
                bool upgradeOption = Random.Range(0, 2) == 0;
                if (upgradeOption)
                {
                    SetUpEquipNew(column);
                }
                else
                {
                    SetUpRankUp(column);
                }
                break;

            case UpgradeCase.JustNew:
                SetUpEquipNew(column);
                break;

            case UpgradeCase.JustUpgrade:
                SetUpRankUp(column);
                break;

            case UpgradeCase.None:
                SetUpBlank(column);
                break;
        }
    }

    public void SetUpRankUp(GearUpgradeColumnUI column)
    {

        int randomIndex = UnityEngine.Random.Range(0, upgradableItems.Count);
        Item randomItem = upgradableItems[randomIndex];
        upgradableItems.Remove(randomItem);

        column.itemIcon.sprite = randomItem.baseItemParameters.icon;
        column.button.onClick.RemoveAllListeners();
        column.button.onClick.AddListener(() => OnClickRankUpItem(randomItem));

        column.headerText.text = new string($"Rank up item");
        column.weaponNameText.text = new string($"{randomItem.GetType().Name}");
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)column.weaponNameText.transform);

        if (randomItem is Weapon)
        {
            column.panelRows.SetActive(true);

            Weapon weapon = (Weapon)randomItem;

            Dictionary<string, float> currentParams = weapon.GetParameters(weapon.CurrentRank);
            Dictionary<string, float> nextParams = weapon.GetParameters(weapon.CurrentRank + 1);
      
            foreach(string key in currentParams.Keys)
            {
                GearUpgradeRowUI row = column.GetRow(key);
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
        else if (randomItem is Accessory)
        {
            column.panelTextObject.SetActive(true);

            Accessory accessory = (Accessory)randomItem;
            (StatModifier modifiedStat, float value) = accessory.GetParameters(accessory.CurrentRank);

            string type = GetModifierTypeString(modifiedStat);
            string textInsert = new string($"{type}:\n +{value*100*(accessory.CurrentRank+1)}%  =>  +{value * 100 * (accessory.CurrentRank + 2)}%.");
            column.panelText.text = textInsert;
        }
    }


    public void SetUpEquipNew(GearUpgradeColumnUI column)
    {
        int randomIndex = UnityEngine.Random.Range(0, equippableItems.Count);
        Item randomItem = equippableItems[randomIndex];
        equippableItems.Remove(randomItem);

        column.itemIcon.sprite = randomItem.baseItemParameters.icon;
        column.panelTextObject.SetActive(true);
        column.button.onClick.AddListener(() => OnClickEquipItem(randomItem));

        column.headerText.text = new string($"Equip NEW item");
        column.weaponNameText.text = new string($"{randomItem.GetType().Name}");
        column.panelText.text = randomItem.baseItemParameters.description;

        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)column.weaponNameText.transform);
    }

    public void SetUpBlank(GearUpgradeColumnUI column)
    {
        column.panelTextObject.SetActive(true);
        
        column.panelTextObject.GetComponent<TextMeshProUGUI>().text = new string($"Baraninka sie skonczyla zostal tylko falafel");

        column.button.onClick.AddListener(() => OnClickHealPlayer(player));
    }

    public string GetModifierTypeString(StatModifier modifier) =>
    modifier switch
    {
        StatModifier.MoveSpeedModifier => "Move Speed",
        StatModifier.WeaponSpeedModifier => "Weapon Speed",
        StatModifier.DamageModifier => "Damage Done",
        StatModifier.CooldownModifier => "Cooldown reduction",
        StatModifier.LootingRadius => "Looting Radius",
        _ => "Unknown"
    };
}
