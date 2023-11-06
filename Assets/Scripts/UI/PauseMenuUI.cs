using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private MenuUI myMenu;
    PlayerInputController inputController;

    private void Start()
    {
        GameManager.Instance.OnGamePaused += PauseGame;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGamePaused -= PauseGame;
    }

    public void PauseGame()
    {
        myMenu.Open();
        //Will need update for multiplayer, prob will swap mappings for all players with method from PlayerManager.
        inputController = PlayerManager.Instance.GetPlayerInputController();
        inputController.SwitchActionMap(inputController.inputActions.MenuActions);
    }

    public void UnPauseGame()
    {
        GameManager.Instance.ResumeGame();
        inputController.SwitchActionMap(inputController.inputActions.GameplayActions);
    }
}
