using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class loginController : MonoBehaviour
{
    // navigate to login scene
    public void OnButtonPress()
    {
        SceneManager.LoadScene("login");
    }
}
