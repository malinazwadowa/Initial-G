using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : SingletonMonoBehaviour<AudioManager>, ISaveable
{
    [SerializeField] private GameObject soundPrefab;
    [SerializeField] private AudioMixer myMixer;

    private SO_AudioClipsParameters audioClips;
    
    private bool soundsPaused;
    //private bool settingsUpdated;
    private List<AudioSource> activeSoundSources;

    private AudioMixerGroup soundsGroup;
    private AudioMixerGroup musicGroup;
    private AudioMixerGroup masterGroup;

    //Prob gonna control cooldown of specific sounds independently, 
    //Current exp sound is spot on with 0.05f CD, dmg sound is eh - updated but still a bit eh
    private float cooldown = 0.05f;
    private Dictionary<AudioClipID, float> CooldownsDictionary = new Dictionary<AudioClipID, float>();

    public SaveData SaveMyData()
    {
        AudioSaveData saveData = new AudioSaveData
        {
            masterVolume = GetCurrentVolume(MixerGroup.Master),
            soundsVolume = GetCurrentVolume(MixerGroup.Sounds),
            musicVolume = GetCurrentVolume(MixerGroup.Music),
        };

        return saveData;
    }

    public void LoadMyData(SaveData loadedData)
    {
        AudioSaveData data = (AudioSaveData)loadedData;
        UpdateSettingsFromSaveFile(data);
        Debug.Log("Updated my settings from file AudioManager");
    }

    [Serializable]
    public class AudioSaveData : SaveData
    {
        public float masterVolume;
        public float soundsVolume;
        public float musicVolume;
    }

    private void UpdateSettingsFromSaveFile(AudioSaveData loadedData)
    {
        SetVolume(MixerGroup.Master, loadedData.masterVolume);
        SetVolume(MixerGroup.Sounds, loadedData.soundsVolume);
        SetVolume(MixerGroup.Music, loadedData.musicVolume);
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void Initalize(SO_AudioClipsParameters clipsData)
    {
        StopAllCoroutines();
        SetAudioClipsData(clipsData);

        activeSoundSources = new List<AudioSource>();

        soundsGroup = myMixer.FindMatchingGroups("Sounds")[0];
        musicGroup = myMixer.FindMatchingGroups("Music")[0];
        masterGroup = myMixer.FindMatchingGroups("Master")[0];
        
        soundsPaused = false;

        PlayMusic(AudioClipID.Music);
    }

    public void SetAudioClipsData(SO_AudioClipsParameters data)
    {
        audioClips = data;
    }

    public void SetVolume(MixerGroup groupName, float value)
    {
        float valueAdjusted = Mathf.Log10(value) * 20;
        myMixer.SetFloat(groupName.ToString(), valueAdjusted);
        Debug.Log($"Setting some { groupName} volume");
    }

    public float GetCurrentVolume(MixerGroup groupName)
    {
        float volume;
        myMixer.GetFloat(groupName.ToString(), out volume);
        return Mathf.Pow(10, volume / 20);
    }

    public AudioSource PlaySound(AudioClipID sound, bool looping = false)
    {
        if (!CheckIfCooldownHasPassed(sound)) { return null; }

        GameObject newSoundObject = ObjectPooler.Instance.SpawnObject(soundPrefab, Vector3.zero);
        AudioSource audioSource = newSoundObject.GetComponent<AudioSource>();

        audioSource.clip = audioClips.GetAudioClip(sound);
        audioSource.outputAudioMixerGroup = soundsGroup;
        audioSource.Play();

        activeSoundSources.Add(audioSource);

        if(!looping)
        {
            audioSource.loop = false;
            StartCoroutine(RemoveSoundObject(audioSource));
        }
        else
        {
            audioSource.loop = true;
        }

        return audioSource;
    }
    
    public void PlayMusic(AudioClipID clipID)
    {
        GameObject newSoundObject = ObjectPooler.Instance.SpawnObject(soundPrefab, Vector3.zero);
        AudioSource audioSource = newSoundObject.GetComponent<AudioSource>();
        audioSource.clip = audioClips.GetAudioClip(clipID);
        audioSource.loop = true;
        audioSource.outputAudioMixerGroup = musicGroup;
        audioSource.Play();
    }

    private bool CheckIfCooldownHasPassed(AudioClipID clipID)
    {
        float currentTime = Time.unscaledTime;

        if (CooldownsDictionary.ContainsKey(clipID))
        {
            float lastPlayTime = CooldownsDictionary[clipID];

            if (currentTime - lastPlayTime >= cooldown)
            {
                CooldownsDictionary[clipID] = currentTime;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            CooldownsDictionary.Add(clipID, currentTime);
            return true;
        }
    }

    private IEnumerator RemoveSoundObject(AudioSource audioSource)
    {
        float delay = audioSource.clip.length + 0.1f;

        yield return new WaitForSeconds(delay);

        if (gameObject != null && !soundsPaused)
        {
            activeSoundSources.Remove(audioSource);
            ObjectPooler.Instance.DespawnObject(audioSource.gameObject);
        }
    }
    
    public void StopAllClips()
    {
        List<GameObject> objectsToDespawn = new List<GameObject>(ObjectPooler.Instance.GetAllActiveObjectsOfType(soundPrefab));

        foreach (GameObject gameObject in objectsToDespawn)
        {
            ObjectPooler.Instance.DespawnObject(gameObject);
        }
    }

    public void PauseAllSounds()
    {
        soundsPaused = true;

        foreach(AudioSource audioSource in activeSoundSources)
        {
            audioSource.Pause();
        }
    }

    public void ResumeAllSounds()
    {
        soundsPaused = false;

        foreach (AudioSource audioSource in activeSoundSources)
        {
            audioSource.UnPause();
            RemoveSoundObject(audioSource);
        }
    }

}
