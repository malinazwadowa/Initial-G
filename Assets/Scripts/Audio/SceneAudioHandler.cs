using UnityEngine;

public class SceneAudioHandler : MonoBehaviour
{
    [SerializeField] private AudioClipsData audioClipsData;
    private void Start()
    {
        AudioManager.Instance.Initalize(audioClipsData);
    }
}
