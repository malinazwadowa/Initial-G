using System.Collections.Generic;
using UnityEngine;

public class GearUpgradeControllerUI : MonoBehaviour
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
        inputController.SwitchActionMap(inputController.inputActions.UI);
        player = PlayerManager.Instance.GetPlayer(); //To be replaced by argument either WeaponController or EntirePlayer or at least players ID to provide for GetPlayer()
        myMenu.Open();
    }

    public void Close()
    {
        levelUpSound?.Stop();
        inputController.SwitchActionMap(inputController.inputActions.GameplayActions);
        TimeManager.ResumeTime();
        myMenu.Close();
    }

    private void UpdateEquipmentState()
    {
        upgradableItems = player.ItemController.GetUpgradableItems();
        equippableItems = player.ItemController.GetEquippableItems();
    }

    private UpgradeCase GetUpgradeCase()
    {
        return (equippableItems.Count > 0, upgradableItems.Count > 0) switch
        {
            (true, true) => UpgradeCase.UpgradeOrNew,
            (true, false) => UpgradeCase.JustNew,
            (false, true) => UpgradeCase.JustUpgrade,
            _ => UpgradeCase.None,
        };
    }

    public void PresentEquipmentUpgradeChoices()
    {
        UpdateEquipmentState();
        Utilities.RemoveChildren(content);
        bool buttonSelected = false;

        List<GameObject> columns = new List<GameObject>();
        int amountOfUpgrades = 2;

        for (int i = 0; i < amountOfUpgrades; i++)
        {
            GameObject columnObject = Instantiate(columnPrefab);
            columnObject.transform.SetParent(content.transform, false);

            GearUpgradeColumnUI column = columnObject.GetComponent<GearUpgradeColumnUI>();
            if (!buttonSelected) { column.button.Select(); buttonSelected = true; }
            column.myController = this;

            UpgradeCase upgradeCase = GetUpgradeCase();
            SetUpCase(upgradeCase, column);
        }
    }

    private void SetUpCase(UpgradeCase upgradeCase, GearUpgradeColumnUI column)
    {
        switch (upgradeCase)
        {
            case UpgradeCase.UpgradeOrNew:
                bool upgradeOption = Random.Range(0, 2) == 0;
                if (upgradeOption)
                {
                    column.SetUpEquipNew(GetRandomEquippableItem(), player);
                }
                else
                {
                    column.SetUpRankUp(GetRandomUpgradableItem());
                }
                break;

            case UpgradeCase.JustNew:
                column.SetUpEquipNew(GetRandomEquippableItem(), player);
                break;

            case UpgradeCase.JustUpgrade:
                column.SetUpRankUp(GetRandomUpgradableItem());
                break;

            case UpgradeCase.None:
                column.SetUpBlank(player);
                break;
        }
    }

    private Item GetRandomEquippableItem()
    {
        int randomIndex = Random.Range(0, equippableItems.Count);
        Item randomItem = equippableItems[randomIndex];
        equippableItems.Remove(randomItem);

        return randomItem;
    }

    private Item GetRandomUpgradableItem()
    {
        int randomIndex = Random.Range(0, upgradableItems.Count);
        Item randomItem = upgradableItems[randomIndex];
        upgradableItems.Remove(randomItem);

        return randomItem;
    }
}
