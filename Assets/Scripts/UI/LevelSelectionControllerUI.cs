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
        public string levelName;
        public bool isUnlocked;
    }

    public void SetupLevels()
    {
        levelInfos = new List<LevelInfo>();
        buttons = new List<Button>();

        foreach (Button button in levelButtons.GetComponentsInChildren<Button>())
        {
            buttons.Add(button);
        }

        

        Dictionary <GameLevel, bool> levelUnlockStatus = GameManager.Instance.levelUnlockController.GetCurrentLevelUnlockStatus();
        Debug.Log("levelunlockstatus zassany : "+ levelUnlockStatus.Count);

        foreach (GameLevel level in Enum.GetValues(typeof(GameLevel)))
        {
            levelInfos.Add(new LevelInfo { levelName = level.ToString(), isUnlocked = levelUnlockStatus[level] });
        }

        for(int i=0; i<levelInfos.Count; i++)
        {
            buttons[i].interactable = levelInfos[i].isUnlocked;

            if (buttons[i].interactable)
            {
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = levelInfos[i].levelName;
                int index = i;
                buttons[i].onClick.AddListener(() => mainMenu.LoadLevel(levelInfos[index].levelName));
            }
            else
            {
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
            }
        }

    }

    // Start is called before the first frame update
    void Awake()
    {
        buttons = new List<Button>();
        foreach(Button button in levelButtons.GetComponentsInChildren<Button>())
        {
            buttons.Add(button);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
