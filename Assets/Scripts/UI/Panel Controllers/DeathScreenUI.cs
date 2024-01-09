using UnityEngine;

public class DeathScreenUI : MonoBehaviour
{
    public MenuUI myMenu;
    private PlayerInputController inputController;

    private void OnEnable()
    {
        EventManager.OnPlayerDeath += OpenMenu;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerDeath -= OpenMenu;
    }

    private void OpenMenu()
    {
        myMenu.Open();
        AudioManager.Instance.StopAllClips();
        AudioManager.Instance.PlaySound(AudioClipID.GameOver);
    }

    public void RestartLevel()
    {
        SceneLoadingManager.Instance.ReLoadScene();
        TimeManager.ResumeTime();
    }

    public void ExitToMainMenu()
    {
        SceneLoadingManager.Instance.Load(SceneName.MainMenu);
        TimeManager.ResumeTime();
    }

    public void PauseGame()
    {
        TimeManager.PauseTime();
        inputController = PlayerManager.Instance.GetPlayerInputController();
        inputController.SwitchActionMap(inputController.inputActions.MenuActions);
    }
}
