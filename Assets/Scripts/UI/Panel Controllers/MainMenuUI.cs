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
}
