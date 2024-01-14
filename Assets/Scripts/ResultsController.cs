using System.Collections.Generic;
using TMPro;
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
        UpdateGeneralData();
        GetWeaponResults();
    }
    private void UpdateGeneralData()
    {
        scoreText.text = new string($"Score: {LevelManager.Instance.Score}");
        levelText.text = new string($"Level gained: {player.ExperienceController.CurrentLevel}");
        timeText.text = TextUtilities.FormatTime(LevelManager.Instance.SessionTime);
    }

    private void GetWeaponResults()
    {
        List<GameObject> accessoriesQueue = new List<GameObject>();
        GameObject myRow;

        foreach (Item item in player.ItemController.EquippedItems)
        {
            myRow = Instantiate(itemRow);
            RowComponents rowComponents = myRow.GetComponent<RowComponents>();

            rowComponents.itemImage.sprite = item.baseItemParameters.icon;
            rowComponents.rankText.text = (item.currentRank + 1).ToString();

            int j = item.currentRank;
            for (int i = 0; i < item.baseItemParameters.amountOfRanks; i++)
            {
                GameObject rankGameObject = new GameObject("RankImage");
                rankGameObject.transform.SetParent(rowComponents.ranksGrid.transform, false);

                RawImage rankImage = rankGameObject.AddComponent<RawImage>();
                if (i<=j)
                {
                    rankImage.texture = rowComponents.rankFilled;
                }
                else
                {
                    rankImage.texture = rowComponents.rankEmpty;
                }
            }

            if (GameManager.Instance.gameStatsController.SessionStats.weaponKillCounts.TryGetValue(item.GetType().Name, out int count))
            {
                rowComponents.killCountText.text = TextUtilities.FormatBigNumber(count);
            }
            else
            {
                rowComponents.killCountText.text = "---";
            }

            if (GameManager.Instance.gameStatsController.SessionStats.weaponDamageDone.TryGetValue(item.GetType().Name, out float damageDone))
            {
                rowComponents.damageDoneText.text = TextUtilities.FormatBigNumber(damageDone);

                //float activeTimeInMins = (Time.time - item.GetTimeEquipped()) / 60;
                string formattedDPS = TextUtilities.FormatBigNumber((float)damageDone / (Time.time - item.TimeOfEquipping));
                rowComponents.dpsText.text = $"{formattedDPS} / sec";
            }
            else
            {
                rowComponents.damageDoneText.text = "---";
                rowComponents.dpsText.text = "---";
            }



            if (item is Weapon)
            {
                myRow.transform.SetParent(itemsTable, false);
            }
            else
            {
                accessoriesQueue.Add(myRow);
            }
        }

        foreach(GameObject row in accessoriesQueue)
        {
            row.transform.SetParent(itemsTable, false);
        }
    }
}
