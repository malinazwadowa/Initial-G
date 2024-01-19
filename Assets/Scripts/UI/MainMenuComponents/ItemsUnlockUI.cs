using System.Collections.Generic;
using UnityEngine;

public class ItemsUnlockUI : MonoBehaviour
{
    public GameObject cellPrefab;
    public Transform grid;
    private List<UnlocksCellUI> cellControllers;

    public void PresentData()
    {
        cellControllers = new List<UnlocksCellUI>();
        List<Item> allItems = GameManager.Instance.itemDataController.allItems;
        Utilities.RemoveChildren(grid);

        foreach (Item item in allItems)
        {
            GameObject myCell = Instantiate(cellPrefab);
            RectTransform cellRectTransform = myCell.GetComponent<RectTransform>();
            
            cellRectTransform.SetParent(grid, false);

            UnlocksCellUI cellScript = myCell.GetComponent<UnlocksCellUI>();
            cellScript.SetUp(item);
            cellControllers.Add(cellScript);
        }

        foreach (UnlocksCellUI cellController in cellControllers)
        {
            //Debug.Log($"sprawdzam dla cell controllera czy jest na seen items nie jest {GameManager.Instance.gameStatsController.OverallStats.seenItems.Contains(cellController.myItemType)} i czy jest unlocked {cellController.isMyItemUnlocked}");
            if (!GameManager.Instance.gameStatsController.OverallStats.seenItems.Contains(cellController.myItemType) && cellController.isMyItemUnlocked)
            {
                cellController.HighlightAsNew();
                GameManager.Instance.gameStatsController.OverallStats.seenItems.Add(cellController.myItemType);
            }
        }

    }
}
