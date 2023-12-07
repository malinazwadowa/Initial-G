using System.Collections.Generic;

public class SaveDataOld
{
    public AudioSettingsData settingsData;
    public LevelUnlocks levelUnlocks;

    public SaveDataOld()
    {
        settingsData = new AudioSettingsData();
        levelUnlocks = new LevelUnlocks();
    }
}

public class AudioSettingsData
{
    public float masterVolume;
    public float soundVolume;
    public float musicVolume;

    public AudioSettingsData( )
    {
        if(AudioManager.Instance != null)
        {
            masterVolume = AudioManager.Instance.GetCurrentVolume(MixerGroup.Master);
            soundVolume = AudioManager.Instance.GetCurrentVolume(MixerGroup.Sounds);
            musicVolume = AudioManager.Instance.GetCurrentVolume(MixerGroup.Music);
        }
    }
}

public class LevelUnlocks
{
    public Dictionary<GameLevel, bool> LevelUnlockStatus;

    public LevelUnlocks()
    {
        LevelUnlockStatus = UnlocksManager.Instance.GetCurrentLevelUnlocks();
    }
}
