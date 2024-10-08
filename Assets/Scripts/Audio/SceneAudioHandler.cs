using NaughtyAttributes;
using UnityEngine;

public class SceneAudioHandler : MonoBehaviour
{
    [SerializeField] private AudioClipNameSelector sceneMusic;

    private void Start()
    {
        AudioManager.Instance.Initialize();
        AudioManager.Instance.PlayMusic(sceneMusic.clipName);
    }
}
