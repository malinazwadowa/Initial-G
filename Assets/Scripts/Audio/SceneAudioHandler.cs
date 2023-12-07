using UnityEngine;

public class SceneAudioHandler : MonoBehaviour
{
    [SerializeField] private SO_AudioClipsParameters audioClipsParameters;
    private void Start()
    {
        AudioManager.Instance.Initalize(audioClipsParameters);
    }
}
