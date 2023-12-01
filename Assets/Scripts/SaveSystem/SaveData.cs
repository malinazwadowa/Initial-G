
public class SaveData
{
    public AudioSettingsData settingsData;

    public SaveData()
    {
        settingsData = new AudioSettingsData();
    }
}
public class AudioSettingsData
{
    public float masterVolume;
    public float soundVolume;
    public float musicVolume;

    public AudioSettingsData()
    {
        if(AudioManager.Instance != null)
        {
            masterVolume = AudioManager.Instance.GetCurrentVolume(MixerGroup.Master);
            soundVolume = AudioManager.Instance.GetCurrentVolume(MixerGroup.Sounds);
            musicVolume = AudioManager.Instance.GetCurrentVolume(MixerGroup.Music);
        }
    }
}
