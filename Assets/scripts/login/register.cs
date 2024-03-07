using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class register : MonoBehaviour
{
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<BsonDocument> collection;


    private string email;
    private string password;
    int n, m;
    // Start is called before the first frame update
    void Start()
    {
        // Initialize MongoDB connection
        client = new MongoClient("mongodb+srv://esomeh:ZndxWXyeRBTpe2GG@senecaweb.7jxhv5v.mongodb.net/?retryWrites=true&w=majority");
        database = client.GetDatabase("RegisterDB");
        collection = database.GetCollection<BsonDocument>("users");
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
        var newUser = new BsonDocument
        {
            {"email", email},
            {"password", password} // Consider hashing the password before storing
        };
        m++;
        Debug.Log("Register Button clicked " + m + " times.");
    }
    
}
