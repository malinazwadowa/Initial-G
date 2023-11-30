using UnityEngine;

public class LevelManager : SingletonMonoBehaviour<LevelManager>
{
    public AudioClipID backgroundMusic;

    private void Start()
    {
        Debug.Log("gram muze");
       // AudioManager.Instance.PlayMusic(backgroundMusic);
    }
}
