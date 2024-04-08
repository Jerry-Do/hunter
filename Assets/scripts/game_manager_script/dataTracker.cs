using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Threading.Tasks;
/// <summary>
/// Keep track of metadata like point, point multiplier, enemy killed and weapons picked up
/// </summary>
public class dataTracker : MonoBehaviour
{
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<BsonDocument> collection;

    // Start is called before the first frame update
    private logicManager lm;
    private double point;
    private double pointMultiplier;
    private int enemyKilled;
    private List<string> weaponNames = new List<string>();
    void Start()
    {
        lm = FindObjectOfType<logicManager>();

        // Initialize MongoDB connection
        client = new MongoClient("mongodb+srv://esomeh:ZndxWXyeRBTpe2GG@senecaweb.7jxhv5v.mongodb.net/?retryWrites=true&w=majority");
        database = client.GetDatabase("RegisterDB");
        collection = database.GetCollection<BsonDocument>("RegisterUnityCollection");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void getData()//this function is called when the game ends, move all of this code to Update if you want testing
    {
        
    }
    public void increaseKillCount()
    {
        enemyKilled++;
    }

    public void addWeapon(string name)
    {
        if (!weaponNames.Contains(name))
        {
            weaponNames.Add(name);
        }
    }

    // Call this method to save game data
    public async Task SaveGameData()
    {
        //string userEmail = PlayerPrefs.GetString("UserEmail", "defaultUser@example.com"); // Default if not found
        string email = UserDataHolder.Instance.UserDocument.GetValue("email").AsString;
        logicManager lM = FindAnyObjectByType<logicManager>();
        point = lM.getPoint();
        pointMultiplier = lM.getMultiplier();

        var gameData = new BsonDocument
        {
            {"point", point},
            {"pointMultiplier", pointMultiplier},
            {"enemyKilled", enemyKilled},
            {"weaponNames", new BsonArray(weaponNames)}
        };

        var filter = Builders<BsonDocument>.Filter.Eq("email", email);
        var options = new ReplaceOptions { IsUpsert = true }; // Ensures a new document is inserted if no existing document matches the filter

        try
        {
            var update = Builders<BsonDocument>.Update.Push("data", gameData);
            await collection.UpdateOneAsync(filter, update);
            Debug.Log("Game data appended to user successfully.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error saving game data: " + ex.Message);
            // Consider more sophisticated error handling here
        }

        /*try
        {
            ReplaceOneResult result = await collection.ReplaceOneAsync(filter, gameData, options);
            if (result.IsAcknowledged)
            {
                if (result.MatchedCount > 0)
                {
                    Debug.Log($"Game data updated successfully for email: {email}");
                }
                else
                {
                    Debug.Log($"Game data inserted successfully for email: {email}");
                }
            }
            else
            {
                Debug.LogError("Failed to save game data.");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"An error occurred while saving game data: {ex.Message}");
        }*/
    }
}
