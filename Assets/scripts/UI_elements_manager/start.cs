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
    public void OnButtonPress()
    {
        //Application.LoadLevel(Application.loadedLevel);
        //Time.timeScale = 1f;
        if (logicManager != null)
        {
            logicManager.restartGame();
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
        }
        //SceneManager.LoadScene("SampleScene");
    }
}
