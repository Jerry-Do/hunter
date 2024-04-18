using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pauseMenuController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject controlMenu;
    [SerializeField] GameObject soundMenu;
    [SerializeField] GameObject quitConfirmation;
    public Image pauseButton;
    public gameStateManager gameStateManager;
    public logicManager logicManager;
    private void Start()
    {
        gameStateManager = FindObjectOfType<gameStateManager>();
        logicManager = FindAnyObjectByType<logicManager>();
    }
    // show pause menu
    public void pause()
    {
        pauseMenu.SetActive(true);
        pauseButton.enabled = false;
        Time.timeScale = 0f;
        gameStateManager.IsGamePaused = true; // set pause state, weapon rotation is diabled
    }
    // resume game 
    public void resume()
    {
        pauseMenu.SetActive(false);
        pauseButton.enabled = true;
        Time.timeScale = 1f;
        gameStateManager.IsGamePaused = false; // weapon rotation is enabled
    }
    // quit game 
    public void quit()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(false);
        quitConfirmation.SetActive(true);
    }
    public void ConfirmQuit()
    {
        Time.timeScale = 1f;
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    // resume game 
    public void ConfirmResume()
    {
        pauseMenu.SetActive(false);
        pauseButton.enabled = true;
        quitConfirmation.SetActive(false);
        Time.timeScale = 1f;
        gameStateManager.IsGamePaused = false; // weapon rotation is enabled
    }
    // navigate back to main menu
    public void backToMainMenu()
    {
        Time.timeScale = 1f;
        logicManager.GameOver();
    }
    // go to control menu
    public void control()
    {
        pauseMenu.SetActive(false); // deactive pause menu
        Time.timeScale = 0f;
        controlMenu.SetActive(true);

    }
    // back to pause menu from control menu
    public void backControl()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        controlMenu.SetActive(false); // deactive control menu 
    }
    // go to sound menu
    public void sound()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 0f;
        soundMenu.SetActive(true);
    }
    // back to pause menu from sound menu
    public void backSound()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        soundMenu.SetActive(false); // deactive sound menu
    }

}
