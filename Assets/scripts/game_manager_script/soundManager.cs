using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class soundManager : MonoBehaviour
{
    public static soundManager instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("theme");
    }
    // play music 
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name); ;
        
        if (s == null)
        {
            Debug.Log("sound not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    // stop music
    public void StopMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name); ;

        if (s == null)
        {
            Debug.Log("sound not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Stop();
        }
    }
    // play sound effect
    public void PlaySfx(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("sfx not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }
    // set audio volume
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    // set sound effects volume
    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
