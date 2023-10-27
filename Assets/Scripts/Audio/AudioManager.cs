using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<Sound> m_Sounds;

    private void Awake()
    {
        foreach(Sound s in m_Sounds)
        {
            InitSound(s);
        }
    }

    private void InitSound(Sound sound)
    {
        sound.AudioSource = gameObject.AddComponent<AudioSource>();
        sound.AudioSource.clip = sound.AudioClip;
        sound.AudioSource.volume = sound.Volume;
        sound.AudioSource.pitch = sound.Pitch;
        sound.AudioSource.loop = sound.Loop;
        sound.AudioSource.playOnAwake = false;
    }

    public void AddSound(Sound sound)
    {
        InitSound(sound);
        m_Sounds.Add(sound);
    }

    public void Play(string name)
    {
        Sound s = m_Sounds.Find(sound => sound.Name == name);
        if (s == null)
            return;
        s.AudioSource.Play();
    }
}
