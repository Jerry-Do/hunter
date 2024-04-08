using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class homeButtonController : MonoBehaviour
{
    int n;
    // navigate title screen
    public async void OnButtonPress()
    {
        dataTracker dt = FindObjectOfType<dataTracker>();
        Debug.Log("home");
        try
        {
            await dt.SaveGameData();
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"An error occurred while saving game data: {ex.Message}");
        }
        
        SceneManager.LoadScene("profile");
    }
}
