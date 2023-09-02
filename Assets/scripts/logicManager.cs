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
    public GameObject store;
    public Text ammo;
    public Text score;
    public Text reloadTime;
    public Text houseHealh;
    public Text playerhealth;
    public Text money;
    public Text weaponName;
    //public House house;
    public playerControl player;
    public Text timer;
    private spawner spawnerL;
    private spawner spawnerR;
    public float time = 31;
    public float orginalTime;
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

        //time -= Time.deltaTime;
        timer.text = "Time: " + ((int)time).ToString();
        playerhealth.text = "Health: " + player.ReturnHealth().ToString();
        ammo.text = "Ammo: " + player.ReturnCurrentAmmo().ToString() + "/" + player.ReturnMaxAmmo().ToString();
        reloadTime.text  = "Reload Time : " + player.ReturnReloadTime().ToString();
        score.text = "Score: " + player.score;
        //houseHealh.text = "House Health : " + house.health.ToString();
        money.text = "Money: " + player.ReturnMoney().ToString();
        if(player.ReturnHealth() <= 0)
        {
            GameOver();
        }
        if(time <= 0)
        {
            Time.timeScale = 0;
            store.SetActive(true);
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
