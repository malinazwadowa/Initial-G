using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] MenuUI myMenu;

    public void StartGame()
    {
        SceneLoadingManager.Instance.Load(SceneName.Development);
    }
}
