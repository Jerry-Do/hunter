using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class register : MonoBehaviour
{
    private string email;
    private string password;
    int n, m;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void readEmailInput(string emailInput)
    {
        email = emailInput;
        Debug.Log(email);
    }

    public void readPasswordInput(string passwordInput)
    {
        password = passwordInput;
        Debug.Log(password);
    }
    // navigate login screen
    public void OnLoginButtonPress()
    {
        n++;
        Debug.Log("Login Button clicked " + n + " times.");
        //SceneManager.LoadScene("login");
    }
    // register function
    public void OnRegisterButtonPress()
    {
        m++;
        Debug.Log("Register Button clicked " + m + " times.");
    }
    
}
