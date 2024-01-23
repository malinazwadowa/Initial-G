using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionUI : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform levelButtonList;

    public void LoadGameLevel(SceneName gameLevel)
    {
        GameManager.Instance.ChangeScene(gameLevel);
    }
    
    public void SetUpButtonLogic()
    {
        Utilities.RemoveChildren(levelButtonList);
        Dictionary<SceneName, bool> levelUnlockStatus = GameManager.Instance.LevelUnlockController.GetCurrentLevelUnlockStatus();

        foreach (SceneName level in levelUnlockStatus.Keys)
        {
            GameObject buttonObject = Instantiate(buttonPrefab);
            buttonObject.transform.SetParent(levelButtonList, false);

            Button button = buttonObject.GetComponent<Button>();
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