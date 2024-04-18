using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Threading.Tasks;
using System.Drawing;
/// <summary>
/// Keep track of metadata like point, point multiplier, enemy killed and weapons picked up
/// </summary>
public class dataTracker : MonoBehaviour
{
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<BsonDocument> collection;

    // Start is called before the first frame update
    private double point;
    private double pointMultiplier;
    private int enemyKilled;
    private List<string> weaponNames = new List<string>();
    public static dataTracker instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
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
    // get game data after game over
    public void UpdateGameData(double point, double pointMultiplier, int enemyKilled, List<string> weaponNames)
    {
        this.point = point;
        this.pointMultiplier = pointMultiplier;
        this.enemyKilled = enemyKilled;
        this.weaponNames = new List<string>(weaponNames); // Create a copy if necessary
    }

    // Call this method to save game data
    public async Task SaveGameData()
    {
        string email = UserDataHolder.Instance.UserDocument.GetValue("email").AsString;

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
    }
}
