using UnityEngine;
using UnityEngine.UI;

public class SettingsControllerUI : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSilder;
    [SerializeField] private Slider soundsVolumeSilder;
    [SerializeField] private Slider musicVolumeSilder;

    private SliderValues storedSliderValues;
    [SerializeField] private GameObject saveButtons;
    [SerializeField] private GameObject regularButtons;
    private bool initalValuesSet = false;
    public struct SliderValues
    {
        public float master;
        public float sounds;
        public float music;
    }   
    
    public void SnapshotValues()
    {
        SliderValues currentSliderValues = new SliderValues
        {
            master = AudioManager.Instance.GetCurrentVolume(MixerGroup.Master),
            sounds = AudioManager.Instance.GetCurrentVolume(MixerGroup.Sounds),
            music = AudioManager.Instance.GetCurrentVolume(MixerGroup.Music)
        };

        storedSliderValues = currentSliderValues;
    }

    public void SetSilderValues()
    {
        initalValuesSet = false;
        masterVolumeSilder.value = AudioManager.Instance.GetCurrentVolume(MixerGroup.Master);
        soundsVolumeSilder.value = AudioManager.Instance.GetCurrentVolume(MixerGroup.Sounds);
        musicVolumeSilder.value = AudioManager.Instance.GetCurrentVolume(MixerGroup.Music);
        initalValuesSet = true;
    }

    public void ResetChanges()
    {
        masterVolumeSilder.value = storedSliderValues.master;
        SetMasterVolume(masterVolumeSilder.value);

        soundsVolumeSilder.value = storedSliderValues.sounds;
        SetSoundsVolume(soundsVolumeSilder.value);

        musicVolumeSilder.value = storedSliderValues.music;
        SetMusicVolume(musicVolumeSilder.value);
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

    public void DisableSaveButtons()
    {
        saveButtons.SetActive(false);
        regularButtons.SetActive(true);
    }

    public void EnableSaveButtons()
    {
        if (!initalValuesSet) { return; }
        saveButtons.SetActive(true);
        regularButtons.SetActive(false);
    }
}
