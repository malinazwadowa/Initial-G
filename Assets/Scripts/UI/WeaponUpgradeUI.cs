using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgradeUI : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private TextMeshProUGUI leftText;
    private TextMeshProUGUI rightText;

    List<Weapon> weapons;

    private int leftWeaponId;
    private int rightWeaponId;

    private void OnEnable()
    {
        ExperienceController.OnPlayerLevelUp += OfferWeaponUpgrade;
    }

    private void OnDisable()
    {
        ExperienceController.OnPlayerLevelUp -= OfferWeaponUpgrade;
    }

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        
        leftText = GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(text => text.name == "LeftText");
        rightText = GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(text => text.name == "RightText");
        Deactivate();
    }

    void Update()
    {

    }
    private void Activate()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        Time.timeScale = 0;
    }
    private void Deactivate()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        Time.timeScale = 1;
    }
    private void OfferWeaponUpgrade()
    {
        Player player = PlayerManager.Instance.GetPlayer(); //To be replaced by argument either WeaponContoller or EntirePlayer or at least players ID to provide for GetPlayer()
        weapons = player.weaponController.equippedWeapons;

        leftWeaponId = Random.Range(0, weapons.Count);
        rightWeaponId = leftWeaponId;

        while (rightWeaponId == leftWeaponId)
        {
            rightWeaponId = Random.Range(0, weapons.Count);
        }

        leftText.text = weapons[leftWeaponId].GetType().Name;
        rightText.text = weapons[rightWeaponId].GetType().Name;

        Activate();
    }

    public void UpgradeLeftWeapon()
    {
        Player player = PlayerManager.Instance.GetPlayer();
        player.weaponController.equippedWeapons[leftWeaponId].RankUp();

        Deactivate();
    }

    public void UpgradeRightWeapon()
    {
        Player player = PlayerManager.Instance.GetPlayer();
        player.weaponController.equippedWeapons[rightWeaponId].RankUp();

        Deactivate();
    }
}
