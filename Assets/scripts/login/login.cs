using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using System.Text;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using System.Text.RegularExpressions;

public class Login : MonoBehaviour
{
    private MongoClient client;
    private IMongoCollection<BsonDocument> collection;

    public string email;
    public string password;
    [SerializeField] GameObject EmailWarning;
    [SerializeField] GameObject EmailNotValid;
    [SerializeField] GameObject PasswordWarning;
    EventSystem system;
    public Selectable firstInput;
    public Button submitButton;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize MongoDB connection
        client = new MongoClient("mongodb+srv://esomeh:ZndxWXyeRBTpe2GG@senecaweb.7jxhv5v.mongodb.net/?retryWrites=true&w=majority");
        var database = client.GetDatabase("RegisterDB");
        collection = database.GetCollection<BsonDocument>("RegisterUnityCollection");
        system = EventSystem.current;
        firstInput.Select();
        EmailWarning.SetActive(false);
        PasswordWarning.SetActive(false);
        EmailNotValid.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Selectable prev = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (prev != null)
            {
                prev.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
            {
                next.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return) && ValidateEmail(email))
        {
            submitButton.onClick.Invoke();
        }
        
    }

    public void readEmailInput(string emailInput)
    {
        email = emailInput;
        if (ValidateEmail(email) == false)
        {
            EmailNotValid.SetActive(true);
        }
        else
        {
            EmailNotValid.SetActive(false);
           
        }
    }

    public void readPasswordInput(string passwordInput)
    {
        password = passwordInput;
    }

    public string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    public async void OnLoginButtonPress()
    {
        EmailWarning.SetActive(false);
        PasswordWarning.SetActive(false);
        var filter = Builders<BsonDocument>.Filter.Eq("email", email);
        var user = await collection.Find(filter).FirstOrDefaultAsync();

        if (user != null)
        {
            // Email found, now check the password
            string storedHashedPassword = user.GetValue("password").AsString;
            string hashedPassword = HashPassword(password);

            if (hashedPassword == storedHashedPassword)
            {
                Debug.Log("Login successful.");
                // Set user data
                UserDataHolder.Instance.UserDocument = user;
                SceneManager.LoadScene("profile");

                PlayerPrefs.SetInt("BindingsModified", 0); // Reset the flag 
                PlayerPrefs.Save();
            }
            else
            {
                PasswordWarning.SetActive(true);
                Debug.LogError("Invalid password.");
            }
        }
        else
        {
            EmailWarning.SetActive(true);
            Debug.LogError("Email not found.");
        }
    }
    public void OnRegisterButtonPress()
    {
        Debug.Log("Register Button clicked ");
        SceneManager.LoadScene("register");
    }

   
    public const string EmailPattern =
    @"^([a-zA-Z0-9]+(?:[._-][a-zA-Z0-9]+)*)@" + // Local part: Starts with alphanumeric, allows '.', '_', and '-' as separators within.
    @"(([a-zA-Z0-9]+(?:-[a-zA-Z0-9]+)*\.)+" + // Subdomains: Allows '-' as separator within, followed by a period.
    @"[a-zA-Z]{2,})$"; // TLD: At least two characters long.



    // validate an email address using the regex pattern defined above
    public static bool ValidateEmail(string email)
    {
        // Check if the email is not null or empty to proceed with regex matching
        if (!string.IsNullOrEmpty(email))
        {
            return Regex.IsMatch(email, EmailPattern);
        }
        else
        {
            // Return false if the email is null, meaning it's not a valid email address
            return false;
        }
    }

}
