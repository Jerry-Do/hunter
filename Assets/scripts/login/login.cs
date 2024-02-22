using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class login : MonoBehaviour
{
    private string email;
    private string password;
    int n, m, i;
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
    // login function
    public void OnLoginButtonPress()
    {
        n++;
        Debug.Log("Login Button clicked " + n + " times.");
    }
    // navigate register page
    public void OnRegisterButtonPress()
    {
        m++;
        Debug.Log("Register Button clicked " + m + " times.");
        //SceneManager.LoadScene("register");
    }
    // forgot password login
    public void OnForgotPasswordButtonPress()
    {
        i++;
        Debug.Log("Forgot Password Button clicked " + i + " times.");
        //SceneManager.LoadScene("");
    }
}
