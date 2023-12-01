using UnityEngine;

public class SceneAudioHandler : MonoBehaviour
{
    [SerializeField] private AudioClipsParameters audioClipsParameters;
    private void Start()
    {
        AudioManager.Instance.Initalize(audioClipsParameters);
    }
}
