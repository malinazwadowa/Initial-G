using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellController : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private Sprite lockedImage;
    [SerializeField] private RawImage panelBackground;
    [SerializeField] private RawImage itemBackground;

    [SerializeField] private GameObject slider;
    [SerializeField] private Image sliderFill;
    [SerializeField] private Image sliderBackground;

    [SerializeField] private Color unlockedColor;
    [SerializeField] private Color lockedColor;
    [SerializeField] private Color weaponColor;
    [SerializeField] private Color accessoryColor;

    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI panelText;
    [SerializeField] private TextMeshProUGUI progressText;

    public void SetUp(Item item)
    {
        GameStatsController statsController = GameManager.Instance.gameStatsController;
        ConditionType conditionType = item.baseItemParameters.unlockCondition.conditionType;
        UnlockCondition condition = item.baseItemParameters.unlockCondition;

        itemIcon.sprite = item.baseItemParameters.icon;
        titleText.text = item.GetType().Name;
        panelText.text = item.baseItemParameters.description;
        slider.SetActive(false);

        if (item is Weapon)
        {
            itemBackground.color = weaponColor;
        }
        else
        {
            itemBackground.color = accessoryColor;
        }

        if (conditionType == ConditionType.UnlockedByDefault)
        {

        }

        else if (conditionType == ConditionType.UnlockedWithEnemyKilled)
        {
            int currentVale = statsController.GetEnemyKilledCountOfType(condition.enemyType);
            
            if( currentVale < condition.amount)
            {
                panelText.text = new string($"Unlocked by killing enemy called: {condition.enemyType}");

                sliderFill.fillAmount = (float)currentVale / condition.amount;
                progressText.text = string.Format("{0}/{1}", currentVale, condition.amount);
                slider.SetActive(true);

                SetToLocked();
            }
        }

        else if (conditionType == ConditionType.UnlockedWithWeaponKills)
        {
            int killCount = statsController.GetWeaponKillCount(condition.weaponType);
            if ( killCount < condition.amount)
            {
                panelText.text = new string($"Unlocked by killing enemies with the {condition.weaponType}.");

                sliderFill.fillAmount = (float)killCount / condition.amount;
                progressText.text = string.Format("{0}/{1}", (float)killCount, condition.amount);
                slider.SetActive(true);

                SetToLocked();
            }
            
        }

        else if (conditionType == ConditionType.UnlockedWithMaxRankOfAccessory)
        {
            if (!statsController.gameStats.itemsFullyRankedUp.Contains(condition.accessoryType))
            {
                panelText.text = new string($"Unlocked by reaching max rank of: {condition.accessoryType}");

                SetToLocked();
            }
        }

        else if (conditionType == ConditionType.UnlockedWithMaxRankOfWeapon)
        {
            if (!statsController.gameStats.itemsFullyRankedUp.Contains(condition.weaponType))
            {
                panelText.text = new string($"Unlocked by reaching max rank of: {condition.weaponType}");

                SetToLocked();
            }
        }

        else
        {
        }
    }

    private void SetToLocked()
    {
        titleText.text = new string("???");
        itemIcon.sprite = lockedImage;
        panelBackground.color = lockedColor;
    }
}
