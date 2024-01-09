using System.Collections.Generic;
using UnityEngine;

public class ItemsUnlockPanelUI : MonoBehaviour
{
    public GameObject cellPrefab;
    public Transform grid;

    public void DoThing()
    {
        List<Item> list = GameManager.Instance.itemDataController.allItems;
        Utilities.RemoveChildren(grid);

        foreach (Item item in list)
        {
            GameObject myCell = Instantiate(cellPrefab);
            RectTransform cellRectTransform = myCell.GetComponent<RectTransform>();
            
            cellRectTransform.SetParent(grid, false);

            myCell.GetComponent<CellController>().SetUp(item);
        }
    }
}
