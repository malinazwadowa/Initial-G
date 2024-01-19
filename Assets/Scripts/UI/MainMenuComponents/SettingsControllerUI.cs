using UnityEngine;
using UnityEngine.UI;

public class SettingsControllerUI : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider soundsVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;

    [SerializeField] private GameObject saveButtons;
    [SerializeField] private GameObject regularButtons;

    private VolumeValues storedVolumeValues;
    private bool initalValuesSet;

    private struct VolumeValues
    {
        public float master;
        public float sounds;
        public float music;
    }

    private void OnEnable()
    {
        initalValuesSet = false;    
        
        SnapshotVolumeValues();
        SetSilderValues();

        initalValuesSet = true;
    }

    public void CancelChanges()
    {
        ResetChanges();
        DisableSaveButtons();
    }

    public void SubmitChanges()
    {
        SnapshotVolumeValues();
        DisableSaveButtons();
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

    public void EnableSaveButtons()
    {
        if (!initalValuesSet) { return; }
        saveButtons.SetActive(true);
        regularButtons.SetActive(false);
    }

    private void DisableSaveButtons()
    {
        saveButtons.SetActive(false);
        regularButtons.SetActive(true);
    }

    private void SnapshotVolumeValues()
    {
        VolumeValues currentVolumeValues = new VolumeValues
        {
            master = AudioManager.Instance.GetCurrentVolume(MixerGroup.Master),
            sounds = AudioManager.Instance.GetCurrentVolume(MixerGroup.Sounds),
            music = AudioManager.Instance.GetCurrentVolume(MixerGroup.Music)
        };

        storedVolumeValues = currentVolumeValues;
    }

    private void SetSilderValues()
    {
        masterVolumeSlider.value = AudioManager.Instance.GetCurrentVolume(MixerGroup.Master);
        soundsVolumeSlider.value = AudioManager.Instance.GetCurrentVolume(MixerGroup.Sounds);
        musicVolumeSlider.value = AudioManager.Instance.GetCurrentVolume(MixerGroup.Music);
    }

    private void ResetChanges()
    {
        masterVolumeSlider.value = storedVolumeValues.master;
        soundsVolumeSlider.value = storedVolumeValues.sounds;
        musicVolumeSlider.value = storedVolumeValues.music;
    }
}
