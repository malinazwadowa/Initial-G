using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    
    public AudioClip explosion;
    public AudioClip throwFyk;
    public AudioClip death;
    public AudioClip ahh;
    public AudioClip augh;
    public AudioClip htsz1;
    public AudioClip htsz2;
    public AudioClip huh;
    public AudioClip shoulder;
    public AudioClip stealth;
    public AudioClip tuptup;
    public AudioClip tuptupFast;

    private AudioSource audioSource;


    public static AudioManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        
    }
    public void PlaySound(string clipName, AudioSource source)
    {
        AudioClip clip = GetAudioClip(clipName);
        if (clip != null)
        {
            source.clip = clip;
            source.Play();
            Debug.Log("dupa");
        }
        else
        {
            Debug.LogWarning("Audio clip not found: " + clipName);
        }
    }
    private AudioClip GetAudioClip(string clipName)
    {
        switch (clipName)
        {
            case "explosion":
                return explosion;
            case "throwFyk":
                return throwFyk;
            case "death":
                return death;
            case "ahh":
                return ahh;
            case "augh":
                return augh;
            case "htsz1":
                return htsz1;
            case "htsz2":
                return htsz2;
            case "huh":
                return huh;
            case "shoulder":
                return shoulder;
            case "stealth":
                return stealth;
            case "tuptup":
                return tuptup;
            case "tuptupFast":
                return tuptupFast;
            default:
                return null;
        }
    }
    
}
