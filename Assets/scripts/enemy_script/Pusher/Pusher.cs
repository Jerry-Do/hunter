using System.Collections;
using System.Collections.Generic;
using MongoDB.Driver;
using UnityEngine;
using static PlayerObj; // Make sure PlayerObj is correctly defined in your project.

public class Pusher : enemy
{

    private enum state
    {
        run,
        attack
    }


    [SerializeField] private float speed = 4.0f;
    public int damage = 1;

    private Pusher()
    {
        health = 15;
        point = 1;


    }

    private state enemyState;

    public Animator sprite;
    private animationController ac;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {


        ac = GetComponent<animationController>();
        EnemyLogic();

        if (health <= 0)
        {
            Destroy(gameObject);
            return; // Avoids further execution after destruction.
        }

        // Flipping direction based on the player's position.
        if ((player.transform.position.x - transform.position.x) <= 0)
        {
            // Enemy faces left.
            transform.eulerAngles = Vector3.forward * 0;
        }
        else
        {
            // Enemy faces right.
            transform.eulerAngles = Vector3.up * 180;
        }

        
        if (Vector2.Distance(transform.position, player.position) > 1)
        {
            enemyState = state.run;
        }
        else if (Vector2.Distance(transform.position, player.position) <= 1f)
        {
            enemyState = state.attack;
        }






    }

    

    

    public override void minusHealth(int damage)
    {
        health -= damage;
        // Handle health reducing logic...
    }

    private void EnemyLogic()
    {
        switch (enemyState)
        {
            case state.run:
                ac.PlayStateAnimation("run");
                if (Vector2.Distance(transform.position, player.transform.position) > 1f)
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                break;
            case state.attack:
                ac.PlayStateAnimation("attack");
                playerControl Hplayer = GameObject.FindGameObjectWithTag("Player").GetComponent<playerControl>();
                if (Hplayer != null)
                {
                    Hplayer.minusHealth(damage);
                }
                break;
        }
    }



}