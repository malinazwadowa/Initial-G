using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GearUpgradeControllerUI : MonoBehaviour
{
    public List<GameObject> panels;

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

        foreach(GameObject panel in panels)
        {
            UpgradeCase upgradeCase = GetUpgradeCase();
            SetUpOptions(upgradeCase, panel);
        }
    }

    private void SetUpOptions(UpgradeCase upgradeCase, GameObject panel)
    {
        switch (upgradeCase)
        {
            case UpgradeCase.UpgradeOrNew:

                bool upgradeOption = UnityEngine.Random.Range(0, 2) == 0;
                if (upgradeOption)
                {
                    SetUpEquipNew(panel);
                }
                else
                {
                    SetUpRankUp(panel);
                }
                break;

            case UpgradeCase.JustNew:
                SetUpEquipNew(panel);
                break;

            case UpgradeCase.JustUpgrade:
                SetUpRankUp(panel);
                break;

            case UpgradeCase.None:
                SetUpBlank(panel);
                break;
        }
    }

    public void SetUpRankUp(GameObject panel)
    {
        int randomIndex = UnityEngine.Random.Range(0, upgradableItems.Count);
        Item randomItem = upgradableItems[randomIndex];
        upgradableItems.Remove(randomItem);

        Button button = panel.GetComponentInChildren<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => OnClickRankUpItem(randomItem));

        TextMeshProUGUI text = panel.transform.Find("PanelText").GetComponent<TextMeshProUGUI>();
        text.text = new string($"Rank up item: {randomItem.GetType().Name}");
    }

    public void SetUpEquipNew(GameObject panel)
    {
        int randomIndex = UnityEngine.Random.Range(0, equippableItems.Count);
        Item randomItem = equippableItems[randomIndex];
        equippableItems.Remove(randomItem);

        Button button = panel.GetComponentInChildren<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => OnClickEquipItem(randomItem));

        TextMeshProUGUI text = panel.transform.Find("PanelText").GetComponent<TextMeshProUGUI>();
        text.text = new string($"Equip NEW item: {randomItem.GetType().Name}");
    }

    public void SetUpBlank(GameObject panel)
    {
        Button button = panel.GetComponentInChildren<Button>();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => Close());

        TextMeshProUGUI text = panel.transform.Find("PanelText").GetComponent<TextMeshProUGUI>();

        text.text = new string($"Baraninka sie skonczyla zostal tylko falafel");
    }
}
