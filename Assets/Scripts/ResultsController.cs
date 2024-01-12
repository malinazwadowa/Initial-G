using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ResultsController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI levelText;
    public GameObject itemRow;
    public Transform itemsTable;
    
    //should be replaced in case of multiple players
    private Player player;
    
    public void PresentResults()
    {
        player = PlayerManager.Instance.GetPlayer();
        UpdateTexts();
        GetWeaponResults();
    }
    private void UpdateTexts()
    {
        scoreText.text = new string($"Score: {LevelManager.Instance.Score}");
        timeText.text = $"{(int)LevelManager.Instance.SessionTime/60}:{(int)LevelManager.Instance.SessionTime % 60}";
    }

    private void GetWeaponResults()
    {
        List<GameObject> queue = new List<GameObject>();
        GameObject myRow;

        foreach (Item item in player.ItemController.EquippedItems)
        {
            myRow = Instantiate(itemRow);
            RowController rowController = myRow.GetComponent<RowController>();

            rowController.itemImage.sprite = item.baseItemParameters.icon;


            int j = item.currentRank;
            for (int i = 0; i < item.baseItemParameters.amountOfRanks; i++)
            {
                GameObject rankGameObject = new GameObject("RankImage");
                rankGameObject.transform.SetParent(rowController.ranksGrid.transform, false);

                RawImage rankImage = rankGameObject.AddComponent<RawImage>();
                if (i<=j)
                {
                    rankImage.texture = rowController.rankFilled;
                }
                else
                {
                    rankImage.texture = rowController.rankEmpty;
                }
            }


            rowController.rankText.text = (item.currentRank + 1).ToString();
            if (GameManager.Instance.gameStatsController.SessionStats.weaponKillCounts.TryGetValue(item.GetType().Name, out int count))
            {
                rowController.killCountText.text = count.ToString();
            }
            else
            {
                rowController.killCountText.text = string.Empty;
            }


            if(item is Weapon)
            {
                myRow.transform.SetParent(itemsTable, false);
            }
            else
            {
                queue.Add(myRow);
            }
            //ImageConversion itemImage = myRow.transform.Find()
        }

        foreach(GameObject row in queue)
        {
            row.transform.SetParent(itemsTable, false);
        }
    }
}
