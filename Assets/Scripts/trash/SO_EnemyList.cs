using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyList", menuName = "ScriptableObjects/PrefabLists/EnemyList")]
public class SO_EnemyList : ScriptableObject
{
    public List<GameObject> allEnemyPrefabs;
    [HideInInspector] public Dictionary<string, GameObject> enemiesByType = new Dictionary<string, GameObject>();
    private void OnValidate()
    {
        RemoveItemsExceptType(allEnemyPrefabs, typeof(Enemy));
        SetTypesOfEnemies();
    }

    private void SetTypesOfEnemies()
    {
        enemiesByType.Clear();
        foreach(GameObject gameObject in allEnemyPrefabs)
        {
            string type = gameObject.GetComponent<Enemy>().GetType().Name;
            if (!enemiesByType.ContainsKey(type))
            {
                enemiesByType.Add(type, gameObject);
            }
            else
            {
                Debug.Log($"Enemy type list already contains {type}");
            }
        }
    }
    private void RemoveItemsExceptType(List<GameObject> itemList, Type targetType)
    {
        itemList.RemoveAll(item => item != null && !item.TryGetComponent(targetType, out _));
    }
}