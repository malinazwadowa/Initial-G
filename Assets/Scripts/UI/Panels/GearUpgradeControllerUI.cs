using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class GearUpgradeControllerUI : MonoBehaviour
{
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;

    private List<Weapon> equippedWeapons;
    private List<Accessory> equippedAccessories;


    /// <summary>
    /// /
    /// </summary>

    private int accessoriesCount;
    private int weaponsCount;
    private List<Item> upgradableItems = new List<Item>();
    private List<Item> availableItems = new List<Item>();
    private List<Item> equippedItems = new List<Item>();

    private Player player;
    private PlayerInputController inputController;
    /// <summary>
    /// wep and accessory controllers for equpping i guess will see
    /// </summary>



    [SerializeField] private MenuUI myMenu;
    public List<Button> buttons;
    public List<GameObject> panels;
    //public Button leftButton;
    //public Button rightButton;
    private AudioSource levelUpSound;


    private int leftWeaponId;
    private int rightWeaponId;
    
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
        player = PlayerManager.Instance.GetPlayer(); //To be replaced by argument either WeaponContoller or EntirePlayer or at least players ID to provide for GetPlayer()
        myMenu.Open();
    }

    private void Close()
    {
        levelUpSound?.Stop();
        inputController.SwitchActionMap(inputController.inputActions.GameplayActions);
        TimeManager.ResumeTime();
        myMenu.Close();
    }

    private void RankUpItem(Item item)
    {
        item.RankUp();
        Close();
    }

    private void EquipItem(Item item)
    {
        if(item is Weapon weapon)
        {
            Type weaponType = weapon.GetType();

            MethodInfo equipMethod = player.weaponController.GetType().GetMethod("EquipWeapon");
            MethodInfo genericEquipMethod = equipMethod.MakeGenericMethod(weaponType);
            genericEquipMethod.Invoke(player.weaponController, null);
        }
        else if(item is Accessory accessory)
        {
            Type accessoryType = accessory.GetType();

            MethodInfo equipAccessoryMethod = player.accessoryController.GetType().GetMethod("EquipAccessory");
            MethodInfo genericEquipAccessoryMethod = equipAccessoryMethod.MakeGenericMethod(accessoryType);
            genericEquipAccessoryMethod.Invoke(player.accessoryController, null);
        }
        else
        {
            Debug.Log("Failed to equip item");
        }

        inputController.SwitchActionMap(inputController.inputActions.GameplayActions);
        Close();
    }

    private void SetEquipmentState()
    {
        equippedWeapons = player.weaponController.equippedWeapons;
        equippedAccessories = player.accessoryController.equippedAccessories;

        weaponsCount = equippedWeapons.Count;
        accessoriesCount = equippedAccessories.Count;
        
        //List<Item> equippedItems = new List<Item>();
        
        upgradableItems.Clear();
        equippedItems.Clear();
        availableItems.Clear();
        Debug.Log("addeding all items to upgradable items");
        upgradableItems.AddRange(equippedWeapons);
        upgradableItems.AddRange(equippedAccessories);
        Debug.Log("added all items to upgradable items" + upgradableItems.Count);
        equippedItems.AddRange(equippedWeapons);
        equippedItems.AddRange(equippedAccessories);


        List<Item> itemsToRemove = new List<Item>();

        foreach (Item item in upgradableItems)
        {
            Debug.Log(item.baseItemParameters.maxRank);
            Debug.Log(item.currentRank) ;
            if (item.currentRank >= item.baseItemParameters.maxRank - 1 ) 
            {
                Debug.Log("removing item from upgradable" + item.name);
                //upgradableItems.Remove(item); 
                itemsToRemove.Add(item);
            }
        }

        foreach (Item item in itemsToRemove)
        {
            if (upgradableItems.Contains(item)) { upgradableItems.Remove(item); }
        }

        if (weaponsCount < 4)
        {
            List<Weapon> weapons = new List<Weapon>();
            weapons = GameManager.Instance.itemController.GetUnlockedWeapons();

            foreach (Weapon weapon in weapons)
            {
                if (!equippedItems.Any(equippedItem => equippedItem.GetType() == weapon.GetType()))
                {
                    availableItems.Add(weapon);
                }
            }
        }

        if (accessoriesCount < 4)
        {
            List<Accessory> accessories = new List<Accessory>();
            accessories = GameManager.Instance.itemController.GetUnlockedAccessories();

            foreach (Accessory accessory in accessories)
            {
                if (!equippedItems.Any(equippedItem => equippedItem.GetType() == accessory.GetType()))
                {
                    availableItems.Add(accessory);
                }
            }
        }
    }

    private UpgradeCase GetUpgradeCase()
    {
        UpgradeCase upgradeCase;

        if ((!(weaponsCount == 4) || !(accessoriesCount == 4)) && upgradableItems.Count > 0)
        {
            upgradeCase = UpgradeCase.UpgradeOrNew;
        }
        else if ((!(weaponsCount == 4) || !(accessoriesCount == 4)) && upgradableItems.Count == 0)
        {
            upgradeCase = UpgradeCase.JustNew;
        }
        else if ((weaponsCount == 4 && accessoriesCount == 4) && upgradableItems.Count > 0)
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

        SetEquipmentState();

        


        foreach(GameObject panel in panels)
        {
            UpgradeCase upgradeCase = GetUpgradeCase();
            SetUpOptions(upgradeCase, panel);
        }

        //to be moved to other method? and here we iterate through all buttons
        /*
        int randomIndex;
        Item randomItem;
        switch (upgradeCase)
        {
            case UpgradeCase.UpgradeOrNew:

                bool upgradeOption = UnityEngine.Random.Range(0, 2) == 0;
                if(upgradeOption)
                {
                    randomIndex = UnityEngine.Random.Range(0, availableItems.Count);
                    randomItem = availableItems[randomIndex];

                    availableItems.Remove(randomItem);
                    buttons[0].onClick.AddListener(() => EquipItem(randomItem));
                }
                else
                {
                    randomIndex = UnityEngine.Random.Range(0, upgradableItems.Count);
                    randomItem = upgradableItems[randomIndex];

                    upgradableItems.Remove(randomItem);
                    buttons[0].onClick.AddListener(() => RankUpItem(randomItem));
                }
                break;

            case UpgradeCase.JustNew:

                randomIndex = UnityEngine.Random.Range(0, availableItems.Count);
                randomItem = availableItems[randomIndex];

                availableItems.Remove(randomItem);
                buttons[0].onClick.AddListener(() => EquipItem(randomItem));
                break;

            case UpgradeCase.JustUpgrade:

                randomIndex = UnityEngine.Random.Range(0, upgradableItems.Count);
                randomItem = upgradableItems[randomIndex];

                upgradableItems.Remove(randomItem);
                buttons[0].onClick.AddListener(() => RankUpItem(randomItem));
                break;

            case UpgradeCase.None:
                //give option of health or money I guess or set flag not to trigger this anymore? 
                break; 
        }*/
    }

    private void SetUpOptions(UpgradeCase upgradeCase, GameObject panel)
    {
        int randomIndex;
        Item randomItem;

        TextMeshProUGUI text;
        string itemType;
        Button button;

        switch (upgradeCase)
        {
            case UpgradeCase.UpgradeOrNew:

                bool upgradeOption = UnityEngine.Random.Range(0, 2) == 0;
                if (upgradeOption && availableItems.Count > 0)
                {
                    randomIndex = UnityEngine.Random.Range(0, availableItems.Count);
                    randomItem = availableItems[randomIndex];

                    availableItems.Remove(randomItem);



                    button = panel.GetComponentInChildren<Button>();

                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(() => EquipItem(randomItem));


                    text = panel.gameObject.transform.Find("PanelText").GetComponent<TextMeshProUGUI>();

                    itemType = randomItem.GetType().Name;

                    text.text = new string($"Equip NEW item: {itemType}");

                }
                else
                {
                    randomIndex = UnityEngine.Random.Range(0, upgradableItems.Count);
                    randomItem = upgradableItems[randomIndex];
                    Debug.Log("wypierdalam upgrdabale bo trafia na liste");
                    upgradableItems.Remove(randomItem);

                    button = panel.GetComponentInChildren<Button>();

                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(() => RankUpItem(randomItem));


                    text = panel.gameObject.transform.Find("PanelText").GetComponent<TextMeshProUGUI>();

                    itemType = randomItem.GetType().Name;

                    text.text = new string($"Rank up item: {itemType}");
                }
                break;

            case UpgradeCase.JustNew:

                randomIndex = UnityEngine.Random.Range(0, availableItems.Count);
                randomItem = availableItems[randomIndex];

                availableItems.Remove(randomItem);


                button = panel.GetComponentInChildren<Button>();

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => EquipItem(randomItem));


                text = panel.gameObject.transform.Find("PanelText").GetComponent<TextMeshProUGUI>();

                itemType = randomItem.GetType().Name;

                text.text = new string($"Equip NEW item: {itemType}");


                break;

            case UpgradeCase.JustUpgrade:

                randomIndex = UnityEngine.Random.Range(0, upgradableItems.Count);
                randomItem = upgradableItems[randomIndex];

                upgradableItems.Remove(randomItem);



                button = panel.GetComponentInChildren<Button>();

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => RankUpItem(randomItem));


                text = panel.gameObject.transform.Find("PanelText").GetComponent<TextMeshProUGUI>();

                itemType = randomItem.GetType().Name;

                text.text = new string($"Rank up item: {itemType}");


                break;

            case UpgradeCase.None:
                //give option of health or money I guess or set flag not to trigger this anymore? 
                Debug.Log("there should be no buttons now but yeahhhhhhhhhhh");

                button = panel.GetComponentInChildren<Button>();

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => Close());


                text = panel.gameObject.transform.Find("PanelText").GetComponent<TextMeshProUGUI>();

                //itemType = randomItem.GetType().Name;

                text.text = new string($"Baraninka sie skonczyla zostal tylko falafel");
                break;
        }
    }
    


    public void UpgradeLeftWeapon()
    {
        //Player player = PlayerManager.Instance.GetPlayer();
        player.weaponController.equippedWeapons[leftWeaponId].RankUp();
        inputController.SwitchActionMap(inputController.inputActions.GameplayActions);
        Close();
    }

    public void UpgradeRightWeapon()
    {
        //Player player = PlayerManager.Instance.GetPlayer();
        player.weaponController.equippedWeapons[rightWeaponId].RankUp();
        inputController.SwitchActionMap(inputController.inputActions.GameplayActions);
        Close();
    }
}
