using UnityEngine;

public class SceneAudioHandler : MonoBehaviour
{
    [SerializeField] private AudioClipsParameters audioClipsData;
    private void Start()
    {
        AudioManager.Instance.Initalize(audioClipsData);
    }
}
