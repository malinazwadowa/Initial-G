using NaughtyAttributes;
using System;
using System.Collections.Generic;

[Serializable]
public class AudioClipNameSelector
{
    private List<string> MusicClipNames => ObjectTypesDatabase.MusicClipNames;
    private List<string> SoundClipNames => ObjectTypesDatabase.SoundClipNames;

    public AudioClipType clipType;

    [Dropdown("GetClipNames")]
    public string clipName;

    private List<string> GetClipNames()
    {
        return clipType == AudioClipType.Music ? MusicClipNames : SoundClipNames;
    }
}
