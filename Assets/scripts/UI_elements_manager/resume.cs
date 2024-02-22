using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class resume : MonoBehaviour
{
    int n;
    // navigate game screen
    public void OnButtonPress()
    {
        n++;
        Debug.Log("Resume Button clicked " + n + " times.");
        //SceneManager.LoadScene("starting");
    }
}
