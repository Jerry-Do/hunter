using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundAudioController : MonoBehaviour
{
    public Slider musicSlider, soundSlider;
    
    public void MusicVolume()
    {
        soundManager.instance.MusicVolume(musicSlider.value);
    }
    public void SFXVolume()
    {
        soundManager.instance.SFXVolume(soundSlider.value);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
