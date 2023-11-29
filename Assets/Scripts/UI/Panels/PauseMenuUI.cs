using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private MenuUI myMenu;
    PlayerInputController inputController;

    private void Start()
    {
        EventManager.OnPauseRequest += OpenMenu;
    }

    private void OnDisable()
    {
        EventManager.OnPauseRequest -= OpenMenu;
    }

    public void OpenMenu()
    {
        myMenu.Open();
    }

    public void PauseGame()
    {
        AudioManager.Instance.PauseAllSounds();
        TimeManager.PauseTime();
        //Will need update for multiplayer, prob will swap mappings for all players with method from PlayerManager.
        inputController = PlayerManager.Instance.GetPlayerInputController();
        inputController.SwitchActionMap(inputController.inputActions.MenuActions);
    }
    
    public void ResumeGame()
    {
        AudioManager.Instance.ResumeAllSounds();
        TimeManager.ResumeTime();
        inputController.SwitchActionMap(inputController.inputActions.GameplayActions);
    }

    public void ExitToMainMenu()
    {
        TimeManager.ResumeTime();
        SceneLoadingManager.Instance.Load(SceneName.MainMenu);
    }

    public void RestartLevel()
    {
        TimeManager.ResumeTime();
        SceneLoadingManager.Instance.ReLoadScene();
    }
}
