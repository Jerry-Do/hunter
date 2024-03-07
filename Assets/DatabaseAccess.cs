using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseAccess : MonoBehaviour
{
    MongoClient client = new MongoClient("mongodb+srv://esomeh:ZndxWXyeRBTpe2GG@senecaweb.7jxhv5v.mongodb.net/?retryWrites=true&w=majority");
    IMongoDatabase database;
    IMongoCollection<BsonDocument> Collection;
    // Start is called before the first frame update
    void Start()
    {
        database = client.GetDatabase("RegisterDB");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
