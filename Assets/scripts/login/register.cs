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

public class register : MonoBehaviour
{
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<BsonDocument> collection;


    public string email;
    public string password;
    int n, m;
    [SerializeField] GameObject EmailNotValid;
    [SerializeField] GameObject EmailNotAvailable;
    EventSystem system;
    public Selectable firstInput;
    public Button submitButton;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize MongoDB connection
        client = new MongoClient("mongodb+srv://esomeh:ZndxWXyeRBTpe2GG@senecaweb.7jxhv5v.mongodb.net/?retryWrites=true&w=majority");
        database = client.GetDatabase("RegisterDB");
        collection = database.GetCollection<BsonDocument>("RegisterUnityCollection");
        system = EventSystem.current;
        // select email input firstly
        firstInput.Select();
        // hide warnings
        EmailNotValid.SetActive(false);
        EmailNotAvailable.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // when receving arrow up input, select upper element 
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Selectable prev = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (prev != null)
            {
                prev.Select();
            }
        }
        // when receving arrow down input, select down element 
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
            {
                
                next.Select();
            }
        }
        // press enter to send login form if email is valid
        else if (Input.GetKeyDown(KeyCode.Return) && ValidateEmail(email))
        {
            submitButton.onClick.Invoke();
        }
    }
    // receive email input
    public void readEmailInput(string emailInput)
    {
        email = emailInput;
        // check if email is not valid, display email valid warning
        if (ValidateEmail(email) == false)
        {
            EmailNotValid.SetActive(true);
        }
        else
        {
            EmailNotValid.SetActive(false);

        }
    }
    // receive password input
    public void readPasswordInput(string passwordInput)
    {
        password = HashPassword(passwordInput);
    }
    // navigate login screen
    public void OnLoginButtonPress()
    {
        SceneManager.LoadScene("login");
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
    // register function
    public async void OnRegisterButtonPress()
    {
        // check if email is valid
        if (ValidateEmail(email))
        {
            EmailNotValid.SetActive(false);
            var filter = Builders<BsonDocument>.Filter.Eq("email", email);
            var user = await collection.Find(filter).FirstOrDefaultAsync();
            // email input is not registered, create new user
            if (user == null)
            {
                EmailNotAvailable.SetActive(false);
                var newUser = new BsonDocument{
                {"email", email},
                {"password", password}
                };
                // insert new user to database
                try
                {
                    await collection.InsertOneAsync(newUser);
                    Debug.Log("User registered successfully.");
                    var filter1 = Builders<BsonDocument>.Filter.Eq("email", email);
                    var user1 = await collection.Find(filter1).FirstOrDefaultAsync();
                    // set user data to data holder
                    UserDataHolder.Instance.UserDocument = user1;
                    SceneManager.LoadScene("profile");
                    PlayerPrefs.SetInt("BindingsModified", 0); // Reset the flag for the next session
                    PlayerPrefs.Save();
                    //PlayerPrefs.SetString("UserEmail", email); // Assuming 'email' is the user's email
                }
                catch (Exception ex)
                {
                    Debug.LogError($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                // display email warning
                EmailNotAvailable.SetActive(true);
                Debug.Log("email is already registered.");
            }
        }
        else
        {
            // display email valid warning
            EmailNotValid.SetActive(true);
        }
    }
    // email pattern
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
