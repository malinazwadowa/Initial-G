using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(SaveableEntity))]
public class AudioManager : SingletonMonoBehaviour<AudioManager>, ISaveable
{
    [SerializeField] private GameObject soundPrefab;
    [SerializeField] private AudioMixer myMixer;

    [Expandable]
    public SO_AudioClipsList audioClipsList;

    private bool soundsPaused;
    private List<AudioSource> activeSoundSources;

    private AudioMixerGroup soundsGroup;
    private AudioMixerGroup musicGroup;
    private AudioMixerGroup masterGroup;

    //Prob gonna control cooldown of specific sounds independently, 
    //Current exp sound is spot on with 0.05f CD, dmg sound is eh - updated but still a bit eh
    private float cooldown = 0.05f;
    private Dictionary<string, float> CooldownsDictionary = new Dictionary<string, float>();

    #region SaveSystemImplementation
    public ObjectData SaveMyData()
    {
        AudioSaveData saveData = new AudioSaveData
        {
            IsProfileIndependent = true,
            masterVolume = GetCurrentVolume(MixerGroup.Master),
            soundsVolume = GetCurrentVolume(MixerGroup.Sounds),
            musicVolume = GetCurrentVolume(MixerGroup.Music),
        };

        return saveData;
    }

    public void LoadMyData(ObjectData loadedData)
    {
        if (loadedData is AudioSaveData audioSaveData)
        {
            UpdateSettingsFromSaveFile(audioSaveData);
        }
    }

    public void WipeMyData()
    {
           
    }

    [Serializable]
    public class AudioSaveData : ObjectData
    {
        public float masterVolume;
        public float soundsVolume;
        public float musicVolume;
    }
    #endregion

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
        audioClipsList.Initialize();
    }

    public void Initialize()
    {
        StopAllCoroutines();

        activeSoundSources = new List<AudioSource>();

        soundsGroup = myMixer.FindMatchingGroups("Sounds")[0];
        musicGroup = myMixer.FindMatchingGroups("Music")[0];
        masterGroup = myMixer.FindMatchingGroups("Master")[0];
        
        soundsPaused = false;
    }

    public void SetVolume(MixerGroup groupName, float value)
    {
        float valueAdjusted = Mathf.Log10(value) * 20;
        myMixer.SetFloat(groupName.ToString(), valueAdjusted);
    }

    public float GetCurrentVolume(MixerGroup groupName)
    {
        float volume;
        myMixer.GetFloat(groupName.ToString(), out volume);
        return Mathf.Pow(10, volume / 20);
    }

    public AudioSource PlaySound(string clipName, bool looping = false)
    {
        if (!CheckIfCooldownHasPassed(clipName)) { return null; }

        GameObject newSoundObject = ObjectPooler.Instance.SpawnObject(soundPrefab, Vector3.zero);
        AudioSource audioSource = newSoundObject.GetComponent<AudioSource>();

        audioSource.clip = audioClipsList.clipsByName[clipName];
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

    public void PlayMusic(string clipName)
    {
        GameObject newSoundObject = ObjectPooler.Instance.SpawnObject(soundPrefab, Vector3.zero);
        AudioSource audioSource = newSoundObject.GetComponent<AudioSource>();
        audioSource.clip = audioClipsList.clipsByName[clipName];
        audioSource.loop = true;
        audioSource.outputAudioMixerGroup = musicGroup;
        audioSource.Play();
    }

    private bool CheckIfCooldownHasPassed(string clipName)
    {
        float currentTime = Time.unscaledTime;

        if (CooldownsDictionary.ContainsKey(clipName))
        {
            float lastPlayTime = CooldownsDictionary[clipName];

            if (currentTime - lastPlayTime >= cooldown)
            {
                CooldownsDictionary[clipName] = currentTime;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            CooldownsDictionary.Add(clipName, currentTime);
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
