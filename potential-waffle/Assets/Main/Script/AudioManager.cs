using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    public void Start()
    {
        Play("BackgroundMusic");
        Play("Thruster");
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);

        if (sound != null)
        {
            sound.source.Play();
        }
        else
        {
            Debug.LogError("SoundNotFound");
        }
    }

    public void ChangeVolume(string name, float volume)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);

        if (sound != null)
        {
            sound.source.volume = volume;
        }
        else
        {
            Debug.LogError("SoundNotFound");
        }
    }
}
