using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemsUnlockPanelUI : MonoBehaviour
{
    public GameObject cellPrefab;
    public Transform grid;
    private List<CellController> cellControllers = new List<CellController>();

    public void PresentData()
    {
        List<Item> allItems = GameManager.Instance.itemDataController.allItems;
        Utilities.RemoveChildren(grid);

        foreach (Item item in allItems)
        {
            GameObject myCell = Instantiate(cellPrefab);
            RectTransform cellRectTransform = myCell.GetComponent<RectTransform>();
            
            cellRectTransform.SetParent(grid, false);

            CellController cellScript = myCell.GetComponent<CellController>();
            cellScript.SetUp(item);
            cellControllers.Add(cellScript);
        }

        foreach (CellController cellController in cellControllers)
        {
            if (!GameManager.Instance.gameStatsController.OverallStats.seenItems.Contains(cellController.myItemType) && cellController.isMyItemUnlocked)
            {
                cellController.HighlightAsNew();
                GameManager.Instance.gameStatsController.OverallStats.seenItems.Add(cellController.myItemType);
            }
        }

    }
}
