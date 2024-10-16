using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAudioClipsList", menuName = "ScriptableObjects/AudioClipsList")]
public class SO_AudioClipsList : ScriptableObject
{
    public List<AudioClip> soundClips;
    public List<AudioClip> musicClips;

    [HideInInspector] public Dictionary<string, AudioClip> clipsByName = new Dictionary<string, AudioClip>();
    [HideInInspector] public List<string> soundClipNames = new List<string>();
    [HideInInspector] public List<string> musicClipNames = new List<string>();

    public void Initialize()
    {
        SetClipNames();
        ObjectTypesDatabase.SetClipsData(soundClipNames, musicClipNames);
    }

    private void OnValidate()
    {
        Initialize();
    }

    private void SetClipNames()
    {
        clipsByName.Clear();
        soundClipNames.Clear();
        musicClipNames.Clear();

        foreach (AudioClip soundClip in soundClips)
        {
            if (!clipsByName.ContainsKey(soundClip.name))
            {
                clipsByName.Add(soundClip.name, soundClip);
                soundClipNames.Add(soundClip.name);
            } 
        }

        foreach (AudioClip musicClip in musicClips)
        {
            if (!clipsByName.ContainsKey(musicClip.name))
            {
                clipsByName.Add(musicClip.name, musicClip);
                musicClipNames.Add(musicClip.name);
            }
        }
    }
}
