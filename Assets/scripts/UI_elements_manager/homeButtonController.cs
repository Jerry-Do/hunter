using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class homeButtonController : MonoBehaviour
{
    int n;
    // navigate title screen
    public void OnButtonPress()
    {
        n++;
        Debug.Log("Home Button clicked " + n + " times.");
        //soundManager.instance.StopMusic("GameOver");
        SceneManager.LoadScene("profile");
    }
}
