using UnityEngine;

public class SceneSoundHandler : MonoBehaviour
{
    [SerializeField] private AudioClipsData audioClipsData;
    private void Start()
    {
        AudioManager.Instance.Initalize(audioClipsData);
    }
}
