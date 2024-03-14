using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using static dropManger;
//using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class logicManager : MonoBehaviour
{
    // Start is called before the first frame update
    //public Text health;
    //Mark the begining of the wave
    //Find a way to mark the end of the wave
    //if the end of a wave pause the game and run spawn weapon
    public GameObject gameOver;
    public Text speedFuel;
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
    [SerializeField] private List<GameObject> weaponsListCommon;
    [SerializeField] private List<GameObject> weaponsListRare;
    [SerializeField] private List<GameObject> weaponsListSuperRare;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider fuelBar;
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
    }

    // Update is called once per frame
    void Update()
    {
        CheckFOrDuplicate();
        //time -= Time.deltaTime;
        timer.text = "Time: " + ((int)time).ToString();
        speedFuel.text = "Fuel: " + player.ReturnFuel().ToString();
        playerhealth.text = "Health: " + player.ReturnHealth().ToString();
        ammo.text = "Ammo: " + player.ReturnCurrentAmmo().ToString() + "/" + player.ReturnMaxAmmo().ToString();

        money.text = "Money: " + player.ReturnMoney().ToString();
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
        spawnerL.enabled = false;
        spawnerR.enabled = false;
        gameOver.SetActive(true);
        // navigate to game over screen
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

    public void spawnRandomWeapon()
    {
        Transform playerSprite = FindAnyObjectByType<rotateSprite>().transform;
        Transform player = FindAnyObjectByType<playerControl>().transform;
        float randomType = UnityEngine.Random.Range(1f, 10f);
        if(randomType <= 5)
        {
            int randomWeapon =  Random.Range(0, weaponsListCommon.Count);
            Instantiate(weaponsListSuperRare[randomWeapon], new Vector3(player.transform.position.x + 5, player.transform.position.y, 0), playerSprite.rotation);
        }
        else if(randomType <= 8)
        {
            int randomWeapon = Random.Range(0, weaponsListCommon.Count);
            Instantiate(weaponsListRare[randomWeapon], new Vector3(player.transform.position.x + 5, player.transform.position.y, 0), playerSprite.rotation);
        }
        else if(randomType <= 10)
        {
            int randomWeapon = Random.Range(0, weaponsListCommon.Count);
            Instantiate(weaponsListCommon[randomWeapon], new Vector3(player.transform.position.x + 5, player.transform.position.y, 0), playerSprite.rotation);
        }
        
         
    }
}
