using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgradeUI : MonoBehaviour
{
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;

    private List<Weapon> weapons;

    private int leftWeaponId;
    private int rightWeaponId;

    [SerializeField] private MenuUI myMenu;
    private Player player;
    PlayerInputController inputController;

    private void OnEnable()
    {
        EventManager.Instance.OnPlayerLevelUp += Open;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnPlayerLevelUp -= Open;
    }

    private void Open()
    {
        inputController = PlayerManager.Instance.GetPlayerInputController();
        inputController.SwitchActionMap(inputController.inputActions.PopUpActions);
        player = PlayerManager.Instance.GetPlayer(); //To be replaced by argument either WeaponContoller or EntirePlayer or at least players ID to provide for GetPlayer()
        myMenu.Open();
    }


    public void OfferWeaponUpgrade()
    {
        
        weapons = player.weaponController.equippedWeapons;

        leftWeaponId = Random.Range(0, weapons.Count);
        rightWeaponId = leftWeaponId;

        while (rightWeaponId == leftWeaponId)
        {
            rightWeaponId = Random.Range(0, weapons.Count);
        }

        leftText.text = weapons[leftWeaponId].GetType().Name;
        rightText.text = weapons[rightWeaponId].GetType().Name;
    }

    public void UpgradeLeftWeapon()
    {
        Player player = PlayerManager.Instance.GetPlayer();
        player.weaponController.equippedWeapons[leftWeaponId].RankUp();
        inputController.SwitchActionMap(inputController.inputActions.GameplayActions);
    }

    public void UpgradeRightWeapon()
    {
        Player player = PlayerManager.Instance.GetPlayer();
        player.weaponController.equippedWeapons[rightWeaponId].RankUp();
        inputController.SwitchActionMap(inputController.inputActions.GameplayActions);
    }

    public void PauseGame()
    {
        TimeManager.Instance.PauseTime();
    }

    public void ResumeGame()
    {
        TimeManager.Instance.ResumeTime();
    }
}
