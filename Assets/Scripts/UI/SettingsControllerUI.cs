using UnityEngine;
using UnityEngine.UI;

public class SettingsControllerUI : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSilder;
    [SerializeField] private Slider soundsVolumeSilder;
    [SerializeField] private Slider musicVolumeSilder;

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
        
        SnapshotValues();
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
        SnapshotValues();
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

    private void SnapshotValues()
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
        masterVolumeSilder.value = AudioManager.Instance.GetCurrentVolume(MixerGroup.Master);
        soundsVolumeSilder.value = AudioManager.Instance.GetCurrentVolume(MixerGroup.Sounds);
        musicVolumeSilder.value = AudioManager.Instance.GetCurrentVolume(MixerGroup.Music);
    }

    private void ResetChanges()
    {
        masterVolumeSilder.value = storedVolumeValues.master;
        soundsVolumeSilder.value = storedVolumeValues.sounds;
        musicVolumeSilder.value = storedVolumeValues.music;
    }
}
