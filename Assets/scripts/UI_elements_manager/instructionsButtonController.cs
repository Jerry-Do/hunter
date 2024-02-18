using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class instructionsButtonController : MonoBehaviour
{
    //int n;
    // navigate intructions screen
    public void OnButtonPress()
    {
        //n++;
        //Debug.Log("Instructions Settings Button clicked " + n + " times.");
        SceneManager.LoadScene("instructions");
    }
}
