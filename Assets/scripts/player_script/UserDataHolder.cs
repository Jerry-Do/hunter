using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson;
using UnityEngine;

public class UserDataHolder : MonoBehaviour
{
    public static UserDataHolder Instance { get; private set; }

    public BsonDocument UserDocument { get; set; }

    public string Email { get; set; }
    public string Name { get; set; }

    // Add other user details you need

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
