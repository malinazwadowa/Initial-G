using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionControllerUI : MonoBehaviour
{
    public GameObject levelButtons;
    public MainMenuUI mainMenu;

    private List<LevelInfo> levelInfos;
    private List<Button> buttons;

    public class LevelInfo
    {
        public GameLevel gameLevel;
        public bool isUnlocked;
    }

    public void LoadGameLevel(GameLevel gameLevel)
    {
        GameManager.Instance.LoadGameLevel(gameLevel);
    }

    public void SetUpButtonLogic()
    {
        levelInfos = new List<LevelInfo>();
        buttons = new List<Button>();

        foreach (Button button in levelButtons.GetComponentsInChildren<Button>())
        {
            buttons.Add(button);
        }

        Dictionary <GameLevel, bool> levelUnlockStatus = GameManager.Instance.levelUnlockController.GetCurrentLevelUnlockStatus();

        foreach (GameLevel level in Enum.GetValues(typeof(GameLevel)))
        {
            levelInfos.Add(new LevelInfo { gameLevel = level, isUnlocked = levelUnlockStatus[level] });
        }

        for(int i=0; i<levelInfos.Count; i++)
        {
            buttons[i].interactable = levelInfos[i].isUnlocked;

            if (buttons[i].interactable)
            {
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = levelInfos[i].gameLevel.ToString();
                int index = i;
                buttons[i].onClick.AddListener(() => LoadGameLevel(levelInfos[index].gameLevel));
            }
            else
            {
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
            }
        }

    }
}
