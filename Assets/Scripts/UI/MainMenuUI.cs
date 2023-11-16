using System;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] MenuUI mainMenu;
    [SerializeField] MenuUI levelSelectionMenu;
    [SerializeField] MenuUI settingsMenu;

    private void Start()
    {
        mainMenu.Open();    
    }

    public void StartGame()
    {
        SceneLoadingManager.Instance.Load(SceneName.Forest);
    }

    public void LoadLevel(string nameOfSceneToLoad)
    {
        string enumTypeName = "SceneName"; // Replace with your actual enum type name
        string enumValueName = nameOfSceneToLoad;

        Type enumType = Type.GetType(enumTypeName);

        if (enumType != null && Enum.IsDefined(enumType, enumValueName))
        {
            object enumValue = Enum.Parse(enumType, enumValueName);
            SceneLoadingManager.Instance.Load((SceneName)enumValue);
        }
        else
        {
            Debug.LogWarning($"Scene named {nameOfSceneToLoad} does not exist.");
        }
    }
}
