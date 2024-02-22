using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class saveController : MonoBehaviour
{
    int n;
    // save settings logic
    public void OnButtonPress()
    {
        n++;
        Debug.Log("Save Button clicked " + n + " times.");
    }
}
