using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionUI : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform levelButtonList;

    public void LoadGameLevel(GameLevel gameLevel)
    {
        GameManager.Instance.SetCurrentGameLevel(gameLevel);
        SO_GameLevel levelData = GameManager.Instance.levelDataController.GetCurrentLevelData();
        GameManager.Instance.ChangeScene(levelData.myScene);
    }
    
    public void SetUpButtonLogic()
    {
        Utilities.RemoveChildren(levelButtonList);
        Dictionary<GameLevel, bool> levelUnlockStatus = GameManager.Instance.levelDataController.GetCurrentLevelUnlockStatus();

        foreach (GameLevel level in levelUnlockStatus.Keys)
        {
            GameObject buttonObject = Instantiate(buttonPrefab);
            buttonObject.transform.SetParent(levelButtonList, false);

            Button button = buttonObject.GetComponentInChildren<Button>();
            button.interactable = levelUnlockStatus[level];

            if (button.interactable)
            {
                buttonObject.GetComponentInChildren<TextMeshProUGUI>().text = level.ToString();
                button.onClick.AddListener(() => LoadGameLevel(level));
            }
            else
            {
                buttonObject.GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
            } 

        }
    }
}