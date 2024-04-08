using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEditor.PackageManager;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ProfileDisplay : MonoBehaviour
{
    public Text emailText;
    public GameObject pointText;
    public GameObject killedEnemy;
    public GameObject pointMultiplierText;
    public GameObject weaponNameText;
    public GameObject separator;
    public Transform pointsParent;
    public Transform weaponsParent;

    private int currentPage = 0;
    private int itemsPerPage = 2; 
    private int totalPages;

    // UI elements for navigation (assign these in the editor)
    public Button nextButton;
    public Button previousButton;

    private MongoClient client;
    private IMongoCollection<BsonDocument> collection;

    // Add other UI elements for user data as needed

    async void Start()
    {
        client = new MongoClient("mongodb+srv://esomeh:ZndxWXyeRBTpe2GG@senecaweb.7jxhv5v.mongodb.net/?retryWrites=true&w=majority");
        var database = client.GetDatabase("RegisterDB");
        collection = database.GetCollection<BsonDocument>("RegisterUnityCollection");
        string email = UserDataHolder.Instance.UserDocument.GetValue("email").AsString;
        var filter = Builders<BsonDocument>.Filter.Eq("email", email);
        var user = await collection.Find(filter).FirstOrDefaultAsync();
        if (user != null)
        {
            emailText.text = "Email: " + UserDataHolder.Instance.UserDocument.GetValue("email").AsString;
            // Attempting to get the data array from the UserDocument
            UserDataHolder.Instance.UserDocument = user;
            var dataArray = UserDataHolder.Instance.UserDocument.GetValue("data", new BsonArray()).AsBsonArray;
         
            // Calculate total pages
            totalPages = Mathf.CeilToInt((float)dataArray.Count / itemsPerPage);
            
            // Display first page
            DisplayPage(currentPage);
        }
    }

    private void DisplayPage(int pageIndex)
    {
        // Clear previous page items if any
        foreach (Transform child in pointsParent)
        {
            Destroy(child.gameObject);
        }
        var dataArray = UserDataHolder.Instance.UserDocument.GetValue("data", new BsonArray()).AsBsonArray;
        // Calculate range of data to display
        int start = pageIndex * itemsPerPage;
        int end = Mathf.Min(start + itemsPerPage, dataArray.Count);

        for (int i = start; i < end; i++)
        {
            // Instantiate UI elements for each item in the range
            var gameDoc = dataArray[i].AsBsonDocument;

            //var gameDoc = gameData.AsBsonDocument;
            double points = gameDoc.GetValue("point", 0.0).ToDouble();
            Debug.Log(points);
            var pointTextInstance = Instantiate(pointText, pointsParent, false);
            pointTextInstance.GetComponent<Text>().text = "Point: " + points;

            double pointsMultiplier = gameDoc.GetValue("pointMultiplier", 0.0).ToDouble();
            var pointMultiplierTextInstance = Instantiate(pointMultiplierText, pointsParent, false);
            pointMultiplierTextInstance.GetComponent<Text>().text = "Point Multiplier: " + pointsMultiplier;

            double enemy = gameDoc.GetValue("enemyKilled", 0.0).ToDouble();
            var enemyTextInstance = Instantiate(killedEnemy, pointsParent, false);
            enemyTextInstance.GetComponent<Text>().text = "Killed Enemies: " + enemy;
            var weaponsArray = gameDoc.GetValue("weaponNames", new BsonArray()).AsBsonArray;

            string weaponNamesString = string.Join(", ", weaponsArray.Select(weapon => weapon.AsString));
            var weaponNameTextInstance = Instantiate(weaponNameText, pointsParent, false);
            weaponNameTextInstance.GetComponent<Text>().text = weaponNamesString;
            /*foreach (var weaponName in weaponsArray)
            {
                weapons += ", " + weaponName;
                var weaponNameTextInstance = Instantiate(weaponNameText, weaponsParent, false);
                weaponNameTextInstance.GetComponent<Text>().text = weaponName.AsString;
            }*/
            var separatorTextInstance = Instantiate(separator, pointsParent, false);
            separatorTextInstance.GetComponent<Text>().text = "---------------------------------";
        }

        // Update navigation buttons
        previousButton.interactable = pageIndex > 0;
        nextButton.interactable = pageIndex < totalPages - 1;
    }

    public void NextPage()
    {
        if (currentPage < totalPages - 1)
        {
            currentPage++;
            DisplayPage(currentPage);
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            DisplayPage(currentPage);
        }
    }
}
