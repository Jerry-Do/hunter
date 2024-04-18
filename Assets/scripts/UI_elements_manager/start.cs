using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class start : MonoBehaviour
{
    public logicManager logicManager;
    private void Start()
    {
        logicManager = FindAnyObjectByType<logicManager>();
    }
    // start new game session
    public void OnButtonPress()
    {   
        if (logicManager != null)
        {
            logicManager.restartGame();
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
