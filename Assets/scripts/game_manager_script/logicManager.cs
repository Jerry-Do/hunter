using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using TMPro;
using System.Data.SqlTypes;
//using static UnityEngine.Rendering.VirtualTexturing.Debugging;
/// <summary>
/// Logic manager for the game. Manages the UI elements on the screen, the enemy spawner 
/// and the point system. Right now also deals with the weapon spawner, might remove 
/// it in the future
/// </summary>
public class logicManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameOver;
    public Text speedFuel;
    private double point = 0;
    [SerializeField] private int time;
    private float timerReal;
    private double pointMultiplier = 1.0f;
    public Text ammo;
    public Text pointText;
    public Text reloadIndicator;
    public Text playerhealth;
    public Text money;
    public TMP_Text pointMultiplierText;
    public TMP_Text timerText;
    public Text weaponName;
    private playerControl player;
    private GameObject weapon;
    private bool waveEnd = false;
    private bool startTimer = false;
    private bool weaponDuplication;
    [SerializeField] private float weaponPickupMul;
    [SerializeField] private List<GameObject> weaponsListCommon;
    [SerializeField] private List<GameObject> weaponsListRare;
    [SerializeField] private List<GameObject> weaponsListSuperRare;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider fuelBar;

    void Start()
    {

        player =  GameObject.FindGameObjectWithTag("Player").GetComponent<playerControl>();
    }

    // Update is called once per frame
    void Update()
    {

        spawner spw = FindAnyObjectByType<spawner>();
        speedFuel.text = "Fuel: " + player.ReturnFuel().ToString();
        playerhealth.text = "Health: " + player.ReturnHealth().ToString();
        ammo.text = "Ammo: " + player.ReturnCurrentAmmo().ToString() + "/" + player.ReturnMaxAmmo().ToString();
        pointText.text = "Point: " + ((int)point).ToString();
        money.text = "Money: " + player.ReturnMoney().ToString();
        pointMultiplierText.text = "Point Multiplier: " + pointMultiplier.ToString();
        healthBar.value = player.ReturnHealth();
        fuelBar.value = player.ReturnFuel();
        if (player.ReturnHealth() <= 0)
        {
            GameOver();
        }
        if(player.ReturnCurrentAmmo() == 0)
        {
            reloadIndicator.text = "Reloading...";
        }
        else
        {
            reloadIndicator.text = "";
        }
        if(startTimer)//Start the timer
        {
            
            spw.setPauseFlag(true);
            timerText.enabled = true;
            timerReal -= Time.deltaTime;
            timerText.text = "Time: " + ((int)timerReal).ToString();
            if(timerReal == 0)
            {
                startTimer = false;
                timerText.enabled = false;
            }  
            if (weapon.IsDestroyed())//If the spawned weapon picked up
            {
                pointMultiplier += weaponDuplication ? weaponPickupMul * 2 : weaponPickupMul;//if the picked up weapon is the same as the player's weapon, then double the multiplier
                spw.setPauseFlag(false);
                startTimer = false;
            }
            
        }
        else
        {
            timerText.enabled = false;
            spw.setPauseFlag(false);
        }
       
    }
   
    void CheckFOrDuplicate()
    {
        if (GameObject.FindGameObjectsWithTag("UI").Length == 2)
        {
            Destroy(GameObject.FindGameObjectsWithTag("UI")[1]);
        }
        if (GameObject.FindGameObjectsWithTag("logic").Length == 2)
        {
            Destroy(GameObject.FindGameObjectsWithTag("logic")[1]);
        }
        if (GameObject.FindGameObjectsWithTag("MainCamera").Length == 2)
        {
            Destroy(GameObject.FindGameObjectsWithTag("MainCamera")[1]);
        }
    }
    void GameOver()
    {
        player.enabled = false;
        gameOver.SetActive(true);
        // navigate to game over screen
        soundManager.instance.PlayMusic("GameOver");
        SceneManager.LoadScene("gameOver");
    }
 
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void addPoint(int amount)
    {
        point +=  amount * pointMultiplier;
    }
    public void setMutiplier(double amount)
    {
        pointMultiplier += amount;
    }
    public void spawnRandomWeapon()
    {
        Transform playerSprite = FindAnyObjectByType<rotateSprite>().transform;
        Transform playerPos = FindAnyObjectByType<playerControl>().transform;
        float randomType = UnityEngine.Random.Range(1f, 10f);
        if(randomType <= 5)
        {
            int randomWeapon =  Random.Range(0, weaponsListCommon.Count);
            weapon = Instantiate(weaponsListSuperRare[randomWeapon], new Vector3(playerPos.transform.position.x + 5, playerPos.transform.position.y, 0), playerSprite.rotation);
        }
        else if(randomType <= 8)
        {
            int randomWeapon = Random.Range(0, weaponsListCommon.Count);
            weapon = Instantiate(weaponsListRare[randomWeapon], new Vector3(playerPos.transform.position.x + 5, playerPos.transform.position.y, 0), playerSprite.rotation);
        }
        else if(randomType <= 10)
        {
            int randomWeapon = Random.Range(0, weaponsListCommon.Count);
            weapon = Instantiate(weaponsListCommon[randomWeapon], new Vector3(playerPos.transform.position.x + 5, playerPos.transform.position.y, 0), playerSprite.rotation);
        }
        weapon w = weapon.GetComponent<weapon>();
        weaponDuplication = w.returnName().Equals(player.ReturnWeaponName());//Check if the spawned is the same as the player's weapon
        
        waveEnd = true;
         
    }
    
    public void setTimerFlagToTrue()
    {
        startTimer = true;
        timerReal = time;
    }

    public double getPoint()
    {
        return point;
    }
    public double getMultiplier()
    {
        return pointMultiplier;
    }
}
