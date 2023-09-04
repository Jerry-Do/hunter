using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class shooterEnemyScript : enemy
{
    // Start is called before the first frame update
    private float timer = 0;
    private int score = 1;
    public int health = 10;
    public int rewardMoney = 3;
    public float speed = 1.5f;
    public int enemyDamage = 1;
    private float cooldown = 1.5f;
    public bool change = false;
    private bool isMoving;
    private AudioSource hitSound;
    private GameObject player;
    private playerControl playerControl;
    private Vector3 lastPosition;
    private Transform myTransform;
    public GameObject ammo;
    void Start()
    {

//hitSound = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<playerControl>();
        myTransform = transform;
        lastPosition = myTransform.position;
        isMoving = false;

    }

    // Update is called once per frame
    void Update()
    {
        
 
        float orignalCooldown = cooldown;
        if (health <= 0)
        {
            Destroy(gameObject);
            
            playerControl.addScore(score);
        }
        if (myTransform.position != lastPosition)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        lastPosition = myTransform.position;
        if (Vector3.Distance(player.transform.position, transform.position) > 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, player.transform.position.y), speed * Time.deltaTime);
        }
     
        if (timer < cooldown)
        {
            timer += Time.deltaTime;
        }
        else if(timer >= cooldown && isMoving == false && GameObject.Find("homingAmmo(Clone)") == null)
        {
            Instantiate(ammo, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            timer = 0;
        }


    }
    public override void minusHealth(int damage)
    {
        //hitSound.Play();
        health -= damage;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            playerControl.minusHealth(enemyDamage);
        }

    }
}

