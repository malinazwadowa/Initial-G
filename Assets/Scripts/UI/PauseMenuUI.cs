using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    private bool isPaused;
    private CanvasGroup pauseMenu;
    
    public CanvasGroup mainMenu;
    public CanvasGroup settingsMenu;

    public TextMeshProUGUI menuTitle;

    private void OnEnable()
    {

        PlayerInputController.OnPausePressed += SwitchActivity;
    }
    private void OnDisable()
    {
        PlayerInputController.OnPausePressed -= SwitchActivity;
    }
    

    void Start()
    {
        pauseMenu = GetComponent<CanvasGroup>();
    }

    public void SwitchActivity()
    {
        if (isPaused)
        {
            Deactivate(pauseMenu);
            isPaused = false;
            Time.timeScale = 1f;
        }
        else
        {
            Activate(pauseMenu);
            GoToMainMenu();
            isPaused = true;
            Time.timeScale = 0f;
        }
    }

    private void Activate(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    private void Deactivate(CanvasGroup canvasGroup)
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }

    public void GoToSettingsMenu()
    {
        Deactivate(mainMenu);
        Activate(settingsMenu);
        menuTitle.text = "Settings";
    }
    public void GoToMainMenu()
    {
        Deactivate(settingsMenu);
        Activate(mainMenu);
        menuTitle.text = "Pause";
    }
}
