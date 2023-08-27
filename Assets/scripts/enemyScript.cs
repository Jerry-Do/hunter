using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    private int score = 1;
    public int health = 10;
    public int enemyDamage = 1;
    private AudioSource hitSound;
    public Rigidbody2D rb;

    private GameObject player;
    public int rewardMoney = 3;
    public float speed = 1.0f;
    public playerControl playerControl;
    public bool change = false;
    void Start()
    {
        hitSound = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<playerControl>();
        //Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());

    }
 
    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            playerControl.increaseMoney(rewardMoney); 
            playerControl.addScore(score);
        }
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, player.transform.position.y), speed * Time.deltaTime);
    }
    public void minusHealth(int damage)
    {
        hitSound.Play();
        health-= damage;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            playerControl.minusHealth(enemyDamage);
        }
        
    }
}
