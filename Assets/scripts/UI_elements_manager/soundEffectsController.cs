using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundEffectsController : MonoBehaviour
{
    float soundVolume;
    public Slider mySlider;
    void Update()
    {
       soundVolume = mySlider.value;
       Debug.Log("Current sound Volume: " + mySlider.value);
    }

}
