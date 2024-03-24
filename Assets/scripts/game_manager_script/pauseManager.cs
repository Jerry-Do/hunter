using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenuController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject controlMenu;
    [SerializeField] GameObject soundMenu;
    [SerializeField] GameObject quitConfirmation;
    public gameStateManager gameStateManager;
    private void Start()
    {
        gameStateManager = FindObjectOfType<gameStateManager>();
    }
    // show pause menu
    public void pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameStateManager.IsGamePaused = true; // set pause state, weapon rotation is diabled
    }
    // resume game 
    public void resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameStateManager.IsGamePaused = false; // weapon rotation is enabled
    }
    // quit game 
    public void quit()
    {
        Time.timeScale = 0f;
        Debug.Log("quit");
        pauseMenu.SetActive(false);
        quitConfirmation.SetActive(true);
        //#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        //UnityEditor.EditorApplication.isPlaying = false;
        //#else
        //Application.Quit();
        //#endif  
    }
    public void ConfirmQuit()
    {
        Time.timeScale = 1f;
        Debug.Log("quit");
        #if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    // resume game 
    public void ConfirmResume()
    {
        pauseMenu.SetActive(false);
        quitConfirmation.SetActive(false);
        Time.timeScale = 1f;
        gameStateManager.IsGamePaused = false; // weapon rotation is enabled
    }
    // navigate back to main menu
    public void backToMainMenu()
    {
        Time.timeScale = 0f;
        Debug.Log("mainMenu");
        //SceneManager.LoadScene("starting");
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
