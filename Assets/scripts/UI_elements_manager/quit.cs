using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class quit : MonoBehaviour
{
    int n;
    // navigate confirmation screen
    public void OnButtonPress()
    {
        n++;
        Debug.Log("Quit Button clicked " + n + " times.");
        SceneManager.LoadScene("startNewConfirmation");
    }
}
