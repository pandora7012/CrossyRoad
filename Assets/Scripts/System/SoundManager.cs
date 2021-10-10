using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager :  Singleton<SoundManager>
{
    
    public Sound[] sounds;
    protected override void Awake()
    {
        base.Awake();
        foreach (Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.audioClip;
            s.audioSource.loop = s.loop;
            s.audioSource.pitch = s.pitch;
            s.audioSource.volume = s.volume;
        }
        if (PlayerPrefs.GetInt("Music") == 1)
            Play("Background");
    }

    public void Play(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
                s.audioSource.Play();
        }
    }

    public void Stop(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
                s.audioSource.Stop();
        }
    }
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip audioClip;
    public bool loop;
    [Range(0, 1)]
    public float volume;
    [Range(0, 1)]
    public float pitch;
    [HideInInspector]
    public AudioSource audioSource;
}


