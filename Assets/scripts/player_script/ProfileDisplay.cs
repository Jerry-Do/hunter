using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileDisplay : MonoBehaviour
{
    public Text emailText;
    public Text nameText;
    // Add other UI elements for user data as needed

    void Start()
    {
        if (UserDataHolder.Instance != null)
        {
            emailText.text = "Email: " + UserDataHolder.Instance.UserDocument.GetValue("email").AsString;
            //nameText.text = "Name: " + UserDataHolder.Instance.Name;
            // Set other user details to UI elements
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
