using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSoundsData", menuName = "Data/Sound List")]
public class AudioClipsData : ScriptableObject
{
    
    private void OnValidate()
    {
        foreach (SoundWithID2 soundWithID in listOfSounds)
        {
            SoundsDictionary[soundWithID.clipID] = soundWithID.audioClip;
        }
    } 

    [Header("Base Settings")]
    [SerializeField] private List<SoundWithID2> listOfSounds;
    private Dictionary<AudioClipID, AudioClip> SoundsDictionary = new Dictionary<AudioClipID, AudioClip>();

    public AudioClip GetAudioClip(AudioClipID sound)
    {
        if (SoundsDictionary.TryGetValue(sound, out AudioClip audioClip))
        {
            return audioClip;
        }
        else
        {
            Debug.LogWarning($"AudioClip for Sound - {sound} not found.");
            return null;
        }
    }

}

[System.Serializable]
public class SoundWithID2
{
    public AudioClipID clipID;
    public AudioClip audioClip;
}