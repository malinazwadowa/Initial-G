using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellController : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private Sprite lockedImage;
    [SerializeField] private RawImage panelBackground;
    [SerializeField] private RawImage panelHighlight;
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
    [SerializeField] private TextMeshProUGUI sliderProgressText;

    public void SetUp(Item item)
    {
        GameStatsController statsController = GameManager.Instance.gameStatsController;
        ConditionType conditionType = item.baseItemParameters.unlockCondition.conditionType;
        UnlockCondition condition = item.baseItemParameters.unlockCondition;

        itemIcon.sprite = item.baseItemParameters.icon;
        itemBackground.color = (item is Weapon) ? weaponColor : accessoryColor;
        
        titleText.text = item.GetType().Name;
        panelText.text = item.baseItemParameters.description;
        
        slider.SetActive(false);
        panelHighlight.gameObject.SetActive(false);
        
        if (GameManager.Instance.gameStatsController.OverallStats.unseenItems.Contains(item.GetType().Name))
        {
            panelHighlight.gameObject.SetActive(true);
            GameManager.Instance.gameStatsController.OverallStats.unseenItems.Remove(item.GetType().Name);
            GameManager.Instance.gameStatsController.OverallStats.seenItems.Add(item.GetType().Name);
        }

        switch (conditionType)
        {
            case ConditionType.UnlockedByDefault:
                // Code for UnlockedByDefault condition
                break;

            case ConditionType.UnlockedWithEnemyKilled:
                int currentVale = statsController.GetEnemyKilledCountOfType(condition.enemyType);

                if (currentVale < condition.amount)
                {
                    panelText.text = new string($"Unlocked by killing enemy called: {condition.enemyType}");

                    EnableSlider(currentVale, condition.amount);

                    SetCellToLocked();
                }
                break;

            case ConditionType.UnlockedWithWeaponKills:
                int killCount = statsController.GetWeaponKillCount(condition.weaponType);
                if (killCount < condition.amount)
                {
                    panelText.text = new string($"Unlocked by killing enemies with the {condition.weaponType}.");

                    EnableSlider(killCount, condition.amount);

                    SetCellToLocked();
                }
                break;

            case ConditionType.UnlockedWithMaxRankOfAccessory:
                if (!statsController.OverallStats.itemsFullyRankedUp.Contains(condition.accessoryType))
                {
                    panelText.text = new string($"Unlocked by reaching max rank of: {condition.accessoryType}");

                    SetCellToLocked();
                }
                break;

            case ConditionType.UnlockedWithMaxRankOfWeapon:
                if (!statsController.OverallStats.itemsFullyRankedUp.Contains(condition.weaponType))
                {
                    panelText.text = new string($"Unlocked by reaching max rank of: {condition.weaponType}");

                    SetCellToLocked();
                }
                break;

            case ConditionType.UnlockedWithCollectedItems:
                statsController.OverallStats.collectibleCounts.TryGetValue(condition.collectibleType, out int count);

                if (count < condition.amount)
                {
                    panelText.text = new string($"Unlocked by picking up more of {condition.collectibleType} type collectibles.");

                    EnableSlider(count, condition.amount);

                    SetCellToLocked();
                }
                break;

            default:
                // Default case if the conditionType doesn't match any of the specified cases
                break;
        }

        /*
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

        else if (conditionType == ConditionType.UnlockedWithCollectedItems)
        {
            statsController.gameStats.collectibleCounts.TryGetValue(condition.collectibleType, out int count);

            if (count < condition.amount)
            {
                panelText.text = new string($"Unlocked by picking up more of {condition.collectibleType} type collectibles.");

                sliderFill.fillAmount = (float)count / condition.amount;
                progressText.text = string.Format("{0}/{1}", (float)count, condition.amount);
                slider.SetActive(true);

                SetToLocked();
            }

        }
        else
        {
        } */
    }

    private void SetCellToLocked()
    {
        titleText.text = new string("???");
        itemIcon.sprite = lockedImage;
        panelBackground.color = lockedColor;
    }

    private void EnableSlider(int currentValue, int targetValue)
    {
        sliderFill.fillAmount = (float)currentValue / targetValue;
        sliderProgressText.text = string.Format("{0}/{1}", currentValue, targetValue);
        slider.SetActive(true);
    }
}
