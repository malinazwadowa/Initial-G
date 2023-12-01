using UnityEngine;
using UnityEngine.UI;

public class SettingsControllerUI : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSilder;
    [SerializeField] private Slider soundsVolumeSilder;
    [SerializeField] private Slider musicVolumeSilder;
    
    public void SetSilderValues()
    {
        masterVolumeSilder.value = AudioManager.Instance.GetCurrentVolume(MixerGroup.Master);
        soundsVolumeSilder.value = AudioManager.Instance.GetCurrentVolume(MixerGroup.Sounds);
        musicVolumeSilder.value = AudioManager.Instance.GetCurrentVolume(MixerGroup.Music);
    }

    public void SetMasterVolume(float value)
    {
        AudioManager.Instance.SetVolume(MixerGroup.Master, value);
    }

    public void SetSoundsVolume(float value)
    {
        AudioManager.Instance.SetVolume(MixerGroup.Sounds, value);
    }

    public void SetMusicVolume(float value)
    {
        AudioManager.Instance.SetVolume(MixerGroup.Music, value);
    }


}
