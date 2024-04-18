using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class restart : MonoBehaviour
{
    // navigate profile screen
    public void OnButtonPress()
    {
        SceneManager.LoadScene("profile");
    }
}
