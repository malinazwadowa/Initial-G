using UnityEngine;
using UnityEngine.UI;

public class SettingsControllerUI : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider soundsVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;

    [SerializeField] private GameObject saveButtons;
    [SerializeField] private GameObject regularButtons;

    [SerializeField] private Button backButton;

    private VolumeValues storedVolumeValues;
    private bool initialValuesSet;

    private struct VolumeValues
    {
        public float master;
        public float sounds;
        public float music;
    }

    private void OnEnable()
    {
        initialValuesSet = false;    
        
        SnapshotVolumeValues();
        SetSliderValues();

        initialValuesSet = true;
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
        SaveSystem.Save();
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
        if (!initialValuesSet) { return; }
        saveButtons.SetActive(true);
        regularButtons.SetActive(false);
    }

    private void DisableSaveButtons()
    {
        saveButtons.SetActive(false);
        regularButtons.SetActive(true);
        backButton.Select();
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

    private void SetSliderValues()
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
