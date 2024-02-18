using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class soundSettingsController : MonoBehaviour
{
    //int n;
    // navigate sound settings screen
    public void OnButtonPress()
    {
       // n++;
        //Debug.Log("Sound Settings Button clicked " + n + " times.");
        SceneManager.LoadScene("soundSetting");
    }
}
