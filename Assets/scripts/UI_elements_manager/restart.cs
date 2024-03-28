using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class restart : MonoBehaviour
{
    int n;
    // navigate title screen
    public void OnButtonPress()
    {
        n++;
        Debug.Log("Restart Button clicked " + n + " times.");
        SceneManager.LoadScene("profile");
    }
}
