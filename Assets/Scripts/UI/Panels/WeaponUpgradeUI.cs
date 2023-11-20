using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        EventManager.OnPlayerLevelUp += Open;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerLevelUp -= Open;
    }

    private void Open()
    {
        TimeManager.PauseTime();
        inputController = PlayerManager.Instance.GetPlayerInputController();
        inputController.SwitchActionMap(inputController.inputActions.PopUpActions);
        player = PlayerManager.Instance.GetPlayer(); //To be replaced by argument either WeaponContoller or EntirePlayer or at least players ID to provide for GetPlayer()
        myMenu.Open();
    }

    private void Close()
    {
        TimeManager.ResumeTime();
        myMenu.Close();
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
