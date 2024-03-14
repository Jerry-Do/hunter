using System.Security.Cryptography;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine;

public class Login : MonoBehaviour
{
    private MongoClient client;
    private IMongoCollection<BsonDocument> collection;

    public string email;
    public string password;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize MongoDB connection
        client = new MongoClient("mongodb+srv://esomeh:ZndxWXyeRBTpe2GG@senecaweb.7jxhv5v.mongodb.net/?retryWrites=true&w=majority");
        var database = client.GetDatabase("RegisterDB");
        collection = database.GetCollection<BsonDocument>("RegisterUnityCollection");
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
            }
            else
            {
                Debug.LogError("Invalid password.");
            }
        }
        else
        {
            Debug.LogError("Email not found.");
        }
    }
   

  
}
