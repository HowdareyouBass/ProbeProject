using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string Name;
    public AudioClip AudioClip;

    [Range(0f, 1f)]
    public float Volume;
    [Range(0.1f, 3f)]
    public float Pitch;

    public bool Loop;
    [HideInInspector]
    public AudioSource AudioSource;

    public Sound(AudioClip clip, string name, float volume = 1f, float pitch = 1f)
    {
        AudioClip = clip;
        Name = name;
        Volume = volume;
        Pitch = pitch;
    }
}
