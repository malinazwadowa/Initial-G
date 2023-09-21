using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public LayerMask enemyLayer;
    public List<GameObject> availableWeapons = new List<GameObject>();

    private IWeaponWielder myWeaponWielder;

    private List<Weapon> equippedWeapons = new List<Weapon>();




    //private CombatStats myCombatStats;
    private Weapon weapon;



    public void Init(IWeaponWielder weaponWielder, CombatStats combatStats)
    {
        myWeaponWielder = weaponWielder;
        //myCombatStats = combatStats;
        weapon.SetModifiers(combatStats);


        EquipWeapon<Spear>();
        EquipWeapon<Rock>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            foreach (Weapon weapon in equippedWeapons)
            {
                weapon.RankUp();
            }
        }
        foreach (Weapon weapon in equippedWeapons)
        {
            weapon.WeaponTick();
        }
    }
    private void AddWeapon(Weapon weapon)
    {
        equippedWeapons.Add(weapon);
    }

    public void EquipWeapon<T>() where T : Weapon
    {
        GameObject weaponPrefab = GetWeaponPrefab<T>();
        GameObject weapon = Instantiate(weaponPrefab, transform);
        T weaponScript = weapon.GetComponent<T>();
        weaponScript.Init(myWeaponWielder);
        AddWeapon(weaponScript);
    }
    private GameObject GetWeaponPrefab<T>() where T : Weapon
    {
        GameObject prefab = availableWeapons.Find(weaponPrefab => weaponPrefab.GetComponent<T>() != null);

        if (prefab != null)
        {
            return prefab;
        }
        else
        {
            Debug.LogError($"Weapon prefab for type {typeof(T)} not found in the available list!");
            return null;
        }
    }










    public Transform GetClosestEnemy(Transform transform)
    {
        List<Collider2D> enemiesFound = new List<Collider2D>();
        float scanRadius = 1;

        while (enemiesFound.Count == 0)
        {
            enemiesFound.AddRange(Physics2D.OverlapCircleAll(transform.position, scanRadius, 1 << 6));
            scanRadius *= 2;
            Debug.Log("ScanRadius: " + scanRadius + " Center position: " + transform.position);
            if (scanRadius > 20)
            {
                Debug.LogWarning("No enemies avilable");
                return null;
            }
        }

        List<float> distancesToEnemies = new List<float>();

        foreach (Collider2D enemy in enemiesFound)
        {
            float distance = Vector2.Distance(enemy.transform.position, transform.position);
            distancesToEnemies.Add(distance);
        }

        int indexOfClosestEnemy = MathUtility.GetIndexOfMin(distancesToEnemies);
        return enemiesFound[indexOfClosestEnemy].transform;
    }
}


