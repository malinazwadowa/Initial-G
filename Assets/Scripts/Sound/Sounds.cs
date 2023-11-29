using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField] private List<SoundWithID> listOfSounds;
    private Dictionary<AudioClipID, AudioClip> SoundsDictionary = new Dictionary<AudioClipID, AudioClip>();

    void Awake()
    {
        foreach (SoundWithID soundWithID in listOfSounds)
        {
            SoundsDictionary[soundWithID.sound] = soundWithID.audioClip;
        }
    }

    public AudioClip GetAudioClip(AudioClipID sound)
    {
        if (SoundsDictionary.TryGetValue(sound, out var audioClip))
        {
            return audioClip;
        }
        else
        {
            Debug.LogWarning($"AudioClip for Sound - {sound} not found.");
            return null;
        }
    }

    [System.Serializable]
    private class SoundWithID
    {
        public AudioClipID sound;
        public AudioClip audioClip;
    }
}
