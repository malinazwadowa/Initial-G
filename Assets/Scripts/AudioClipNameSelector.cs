using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AudioClipNameSelector : ISerializationCallbackReceiver
{
    private List<string> MusicClipNames => ItemTypesDatabase.MusicClipNames;
    private List<string> SoundClipNames => ItemTypesDatabase.SoundClipNames;

    public AudioClipType clipType;
    private bool clipIsSound;
    private bool clipIsMusic;

    [AllowNesting]
    [ShowIf(nameof(clipIsSound))]
    [Dropdown(nameof(SoundClipNames))]
    public string soundClipName;

    [AllowNesting]
    [ShowIf(nameof(clipIsMusic))]
    [Dropdown(nameof(MusicClipNames))]
    public string musicClipName;

    [HideInInspector] public string clipName;

    //public void Validate()
    //{
    //    bool isSound = clipType == AudioClipType.Sound;
    //    bool isMusic = clipType == AudioClipType.Music;

    //    if (isSound)
    //    {
    //        clipName = soundClipName;
    //    }
    //    else if (isMusic)
    //    {
    //        clipName = musicClipName;
    //    }
    //}

    public void Validate()
    {
        clipIsSound = false;
        clipIsMusic = false;

        switch (clipType)
        {
            case AudioClipType.Sound:
                clipIsSound = true;
                break;
            case AudioClipType.Music:
                clipIsMusic = true;
                break;
        }


        if (clipIsSound) { clipName = soundClipName; }
        if (clipIsMusic) { clipName = musicClipName; }
        //Debug.Log($"{clipName}");

    }

    public void OnBeforeSerialize()
    {
        Validate();
    }

    public void OnAfterDeserialize()
    {
        
    }
}