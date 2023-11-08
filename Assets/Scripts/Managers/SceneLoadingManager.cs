using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadingManager : SingletonMonoBehaviour<SceneLoadingManager>
{
    [SerializeField] private MenuUI loadingCanvas;
    [SerializeField] private Image progressBar;
    private bool isLoading;
    private SceneName currentSceneName;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void Load(SceneName sceneName)
    {
        if (isLoading)
        {
            Debug.LogWarning("A scene is already being loaded.");
            return;
        }
        StartCoroutine(LoadCoroutine(sceneName));
        currentSceneName = sceneName;
    }

    private IEnumerator LoadCoroutine(SceneName sceneName)
    {
        isLoading = true;
        loadingCanvas.Open();

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName.ToString());
        asyncOperation.allowSceneActivation = false;

        while (asyncOperation.progress < 0.9f)
        {
            progressBar.fillAmount = asyncOperation.progress / 0.9f;
            yield return new WaitForEndOfFrame();
        }

        asyncOperation.allowSceneActivation = true;

        yield return new WaitUntil(() => asyncOperation.isDone == true);


        loadingCanvas.Close();
        isLoading = false;
    }

    public void ReLoadScene()
    {
        Load(currentSceneName);
    }
}