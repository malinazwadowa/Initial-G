using NaughtyAttributes;
using UnityEngine;

public class SceneAudioHandler : MonoBehaviour
{
    [Expandable][SerializeField] private SO_AudioClipsParameters audioClipsParameters;
    private void Start()
    {
        AudioManager.Instance.Initalize(audioClipsParameters);
    }
}
