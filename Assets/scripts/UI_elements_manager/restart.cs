using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class restart : MonoBehaviour
{
    int n;
    public Text text;
    // navigate title screen
    public void OnButtonPress()
    {
        n++;
        text.text = "Restart Button clicked " + n + " times.";
        //SceneManager.LoadScene("starting");
    }
}
