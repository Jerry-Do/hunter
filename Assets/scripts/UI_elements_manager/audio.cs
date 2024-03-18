using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class audio : MonoBehaviour
{
    float audioVolume;
    public Slider mySlider;
    
    void Update()
    {
        audioVolume = mySlider.value;
        Debug.Log("Current audio Volume: " + mySlider.value);
    }
}
