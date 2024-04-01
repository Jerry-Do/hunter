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
        client = new MongoClient("mongodb+srv://esomeh:<password>@senecaweb.7jxhv5v.mongodb.net/?retryWrites=true&w=majority");
        database = client.GetDatabase("Game");
        collection = database.GetCollection<BsonDocument>("RecordScore");
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
        point = lm.getPoint();
        pointMultiplier = lm.getMultiplier();
        var gameData = new BsonDocument
        {
            {"point", point},
            {"pointMultiplier", pointMultiplier},
            {"enemyKilled", enemyKilled},
            {"weaponNames", new BsonArray(weaponNames)}
        };

        try
        {
            await collection.InsertOneAsync(gameData);
            Debug.Log("Game data saved successfully.");
            
        }
        catch (System.Exception ex)
        {
            throw (ex);
            
        }
    }
}
