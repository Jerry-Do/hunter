using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;






public class Solder_Script : enemy
{
    private float timer = 0;
    private int score = 1;
    public int health = 10;
    public int rewardMoney = 3;
    public float speed = 1.5f;
    public int enemyDamage = 1;
    private float cooldown = 1.5f;
    private bool moving;
    private GameObject player;
    private playerControl playerControl;
    private Transform myTransform;
    public GameObject ammoPrefab;
    public Transform firePointLeft;
    public Transform firePointRight;

    private Animator animator;
    private Vector3 previousPosition = Vector3.zero;
    private bool isFacingRight = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerControl = player.GetComponent<playerControl>();
        myTransform = transform;
        previousPosition = myTransform.position;
        moving = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Store the current position



   
        if (myTransform.position != previousPosition)
        {
            moving = true; 
        }
        else
        {
            moving = false; 
        }

      
        
        
        FlipCharacter();
        previousPosition = myTransform.position;

        


        void FlipCharacter()
        {

           
            if (myTransform.position.x < previousPosition.x && isFacingRight)
            {
              
                Flip();
            }
            else if (myTransform.position.x > previousPosition.x && !isFacingRight)
            {
             
                Flip();
            }

            
        }


        void Flip()
        {
         
            isFacingRight = !isFacingRight;

         
            Vector3 scale = transform.localScale;

         
            scale.x *= -1;

       
            transform.localScale = scale;
        }



        if (health <= 0)
        {
            Destroy(gameObject);
            playerControl.addScore(score);
        }

        if (Vector3.Distance(player.transform.position, transform.position) > 2)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        if (timer < cooldown)
        {
            timer += Time.deltaTime;
        }
        else if (timer >= cooldown && !moving && GameObject.Find("homingAmmo(Clone)") == null)
        {
            Shoot();
            timer = 0;
        }

        // Update Animator parameters based on enemy behavior
        animator.SetBool("moving", moving);
    }

    // Method to handle shooting
    void Shoot()
    {
        Debug.Log(isFacingRight);   
        if (!isFacingRight) {
            Instantiate(ammoPrefab, firePointLeft.position, Quaternion.identity);
            animator.SetTrigger("Firing"); // Trigger the fire animation

        }
        else {
            Instantiate(ammoPrefab, firePointRight.position, Quaternion.identity);
            animator.SetTrigger("Firing"); // Trigger the fire animation

        }




    }

    public override void minusHealth(int damage)
    {
        health -= damage;
        animator.SetTrigger("hurt"); // Trigger the hurt animation

        if (health <= 0)
        {
            Destroy(gameObject);
            playerControl.minusHealth(enemyDamage);
            animator.SetTrigger("die"); // Trigger the die animation
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            playerControl.minusHealth(enemyDamage);
            animator.SetTrigger("die"); // Trigger the die animation 
        }
    }
}