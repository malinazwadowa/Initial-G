using UnityEngine;

public class ResultsPanelUI : MonoBehaviour
{
    public MenuUI myMenu;
    private PlayerInputController inputController;
    public ResultsControllerUI resultsController;

    private void OnEnable()
    {
        EventManager.OnPlayerDeath += () => OpenMenu(false);
        EventManager.OnLevelCompleted += () => OpenMenu(true);
    }

    private void OnDisable()
    {
        EventManager.OnPlayerDeath -= () => OpenMenu(false);
        EventManager.OnLevelCompleted -= () => OpenMenu(true);
    }

    private void OpenMenu(bool success)
    {
        myMenu.Open();
        AudioManager.Instance.StopAllClips();
        
        if(success)
        {
            AudioManager.Instance.PlaySound(AudioClipID.GameWon);
        }
        else
        {
            AudioManager.Instance.PlaySound(AudioClipID.GameOver);
        }
        //AudioManager.Instance.PlaySound(AudioClipID.GameOver);
    }

    public void RestartLevel()
    {
        TimeManager.ResetTimeScale();
        GameManager.Instance.ChangeScene(GameManager.Instance.CurrentScene);
    }

    public void ExitToMainMenu()
    {
        GameManager.Instance.ChangeScene(SceneName.MainMenu);
        TimeManager.ResumeTime();
    }

    public void PauseGame()
    {
        TimeManager.PauseTime();
        inputController = PlayerManager.Instance.GetPlayerInputController();
        inputController.SwitchActionMap(inputController.inputActions.MenuActions);
    }
}
