using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
// game state
public class gameStateManager : MonoBehaviour
{
    public static bool IsGamePaused = false;

    public static gameStateManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
