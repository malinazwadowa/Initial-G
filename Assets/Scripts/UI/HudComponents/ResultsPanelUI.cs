using UnityEngine;

public class ResultsPanelUI : MonoBehaviour
{
    public MenuUI myMenu;
    private PlayerInputController inputController;
    public ResultsControllerUI resultsController;

    private void OnEnable()
    {
        EventManager.OnPlayerDeath += myMenu.Open;
        EventManager.OnLevelCompleted += myMenu.Open;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerDeath -= myMenu.Open;
        EventManager.OnLevelCompleted -= myMenu.Open;
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
