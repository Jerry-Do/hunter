using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class logicManager : MonoBehaviour
{
    // Start is called before the first frame update
    //public Text health;

    public GameObject gameOver;
    public Text ammo;
    public Text score;
    public Text reloadIndicator;
    public Text playerhealth;
    public Text money;
    public Text weaponName;
    public playerControl player;
    public Text timer;
    private spawner spawnerL;
    private spawner spawnerR;
    public float time = 31;
    public float orginalTime;
    private logicManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
            return;
        }
        instance = this;
        DontDestroyOnLoad(instance);
    }
    void Start()
    {
        orginalTime = time;
        player =  GameObject.FindGameObjectWithTag("Player").GetComponent<playerControl>();
        spawnerL = GameObject.FindGameObjectWithTag("spawnerL").GetComponent<spawner>();
        spawnerR = GameObject.FindGameObjectWithTag("spawnerR").GetComponent<spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("UI").Length == 2)
        {
            Destroy(GameObject.FindGameObjectsWithTag("UI")[1]);
        }
        if (GameObject.FindGameObjectsWithTag("logic").Length == 2)
        {
            Destroy(GameObject.FindGameObjectsWithTag("logic")[1]);
        }
        //time -= Time.deltaTime;
        timer.text = "Time: " + ((int)time).ToString();
        playerhealth.text = "Health: " + player.ReturnHealth().ToString();
        ammo.text = "Ammo: " + player.ReturnCurrentAmmo().ToString() + "/" + player.ReturnMaxAmmo().ToString();
        score.text = "Score: " + player.score;
        //houseHealh.text = "House Health : " + house.health.ToString();
        money.text = "Money: " + player.ReturnMoney().ToString();
        if(player.ReturnHealth() <= 0)
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
    }
   
    void GameOver()
    {
        player.enabled = false;
        spawnerL.enabled = false;
        spawnerR.enabled = false;
        gameOver.SetActive(true);

    }
    //public void upgradeMobs()
    //{
    //    spawnerL.upgradeMob();
    //    spawnerR.upgradeMob();
    //}
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
}
