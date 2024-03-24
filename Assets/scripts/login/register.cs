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

public class register : MonoBehaviour
{
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<BsonDocument> collection;


    public string email;
    public string password;
    int n, m;

    EventSystem system;
    //public Selectable firstInput;
    public Button submitButton;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize MongoDB connection
        client = new MongoClient("mongodb+srv://esomeh:ZndxWXyeRBTpe2GG@senecaweb.7jxhv5v.mongodb.net/?retryWrites=true&w=majority");
        database = client.GetDatabase("RegisterDB");
        collection = database.GetCollection<BsonDocument>("RegisterUnityCollection");
        system = EventSystem.current;
        //firstInput.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Selectable prev = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (prev != null)
            {
                prev.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
            {
                
                next.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            submitButton.onClick.Invoke();
        }
    }

    public void readEmailInput(string emailInput)
    {
        email = emailInput;
        Debug.Log(email);
    }

    public void readPasswordInput(string passwordInput)
    {
        password = HashPassword(passwordInput);
        Debug.Log(password);
    }
    // navigate login screen
    public void OnLoginButtonPress()
    {
        n++;
        Debug.Log("Login Button clicked " + n + " times.");
        SceneManager.LoadScene("login");
    }
    // register function

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
    public async void OnRegisterButtonPress()
    {
        var newUser = new BsonDocument{
        {"email", email},
        {"password", password}
        };

        try
        {
            await collection.InsertOneAsync(newUser);
            Debug.Log("User registered successfully.");
            var filter = Builders<BsonDocument>.Filter.Eq("email", email);
            var user = await collection.Find(filter).FirstOrDefaultAsync();
            UserDataHolder.Instance.UserDocument = user;
            SceneManager.LoadScene("profile");
        }
        catch (Exception ex)
        {
            Debug.LogError($"An error occurred: {ex.Message}");
        }
    }
}
