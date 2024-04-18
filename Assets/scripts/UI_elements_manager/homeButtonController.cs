using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class homeButtonController : MonoBehaviour
{
    // navigate title screen
    public async void OnButtonPress()
    {
        dataTracker dt = FindObjectOfType<dataTracker>();
        // save game session to database
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
