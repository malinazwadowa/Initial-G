using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultsControllerUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI levelText;
    public GameObject itemRow;
    public Transform itemsTable;
    public TextMeshProUGUI bannerText;

    public AudioClipNameSelector gameLostSound;
    public AudioClipNameSelector gameWonSound;

    private Color red = new Color(199 / 255f, 10 / 255f, 29 / 255f);
    private Color green = new Color(1f / 255f, 104f / 255f, 11f / 255f);

    //should be replaced in case of multiple players
    private Player player;
    
    public void PresentResults()
    {
        player = PlayerManager.Instance.GetPlayer();

        if (!player.IsAlive)
        {
            bannerText.color = red;
            bannerText.text = "You Died...";
            AudioManager.Instance.StopAllClips();
            AudioManager.Instance.PlaySound(gameLostSound.clipName);

        }
        else
        {
            bannerText.color = green;
            bannerText.text = "You Won!";
            AudioManager.Instance.StopAllClips();
            AudioManager.Instance.PlaySound(gameWonSound.clipName);
        }

        UpdateGeneralData();
        GetWeaponResults();
    }
    private void UpdateGeneralData()
    {
        scoreText.text = new string($"Score: {LevelManager.Instance.Score}");
        levelText.text = new string($"Level gained: {player.ExperienceController.CurrentLevel}");
        timeText.text = new string ("Time survived: ") + TextUtilities.FormatTime(LevelManager.Instance.SessionTime);
    }

    private void GetWeaponResults()
    {
        List<GameObject> accessoriesQueue = new List<GameObject>();
        GameObject myRow;

        foreach (Item item in player.ItemController.EquippedItems)
        {
            myRow = Instantiate(itemRow);
            RowComponentsUI rowComponents = myRow.GetComponent<RowComponentsUI>();

            rowComponents.itemImage.sprite = item.baseItemParameters.icon;
            rowComponents.rankText.text = (item.CurrentRank + 1).ToString();

            int j = item.CurrentRank;
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
