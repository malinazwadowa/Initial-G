using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellController : MonoBehaviour
{
    public Sprite lockedImage;
    public RawImage panelBackground;
    public RawImage itemBackground;


    public Color unlockedColor;
    public Color lockedColor;
    public Color weaponColor;
    public Color accessoryColor;

    public Image weaponIcon;

    public GameObject slider;
    public Image sliderFill;
    public Image sliderBackground;
    
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI panelText;
    public TextMeshProUGUI progressText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUp(Item item)
    {
        ConditionType conditionType = item.baseItemParameters.unlockCondition.conditionType;
        
        if(item is Weapon)
        {
            itemBackground.color = weaponColor;
        }
        else
        {
            itemBackground.color = accessoryColor;
        }


        if (conditionType == ConditionType.UnlockedByDefault)
        {
            titleText.text = item.GetType().Name;
            weaponIcon.sprite = item.baseItemParameters.icon;
            panelText.text = item.baseItemParameters.description;
            slider.SetActive(false);
        }
        else if (conditionType == ConditionType.UnlockedWithEnemyKilled)
        {
            weaponIcon.sprite = item.baseItemParameters.icon;
            string enemy = item.baseItemParameters.unlockCondition.enemyType.ToString();
            

            int currentVale = GameManager.Instance.gameStatsController.GetEnemyKilledCountOfType(item.baseItemParameters.unlockCondition.enemyType);
            int targetValue = item.baseItemParameters.unlockCondition.amount;
            
            if( currentVale < targetValue )
            {
                panelText.text = new string($"Unlocked by killing enemy called: {enemy}");

                float dups = (float) currentVale / targetValue;
                sliderFill.fillAmount = dups;
                progressText.text = string.Format("{0}/{1}", currentVale, targetValue);

                titleText.text = new string("???");
                weaponIcon.sprite = lockedImage;
                panelBackground.color = lockedColor;
            }
            else
            {
                panelText.text = item.baseItemParameters.description;
                titleText.text = item.GetType().Name;
                slider.SetActive(false);
            }
            
        }
        else if (conditionType == ConditionType.UnlockedWithWeaponKills)
        {

        }
        else if (conditionType == ConditionType.UnlockedWithMaxRankOfAccessory)
        {
            
        }
        else if (conditionType == ConditionType.UnlockedWithMaxRankOfWeapon)
        {

        }
        else
        {
        }


    }
}
