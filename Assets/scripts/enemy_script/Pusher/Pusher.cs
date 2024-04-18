using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerObj;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Pusher : enemy
{
    // Start is called before the first frame update

    private enum state
    {
        run,
        attack,
        idle
    }
    private state enemyState;
    private AudioSource hitSound;
    private bool rageMode = false;
    public Animator sprite;
    private animationController ac;
    [SerializeField] private float speed = 4.0f;
    private playerControl playerControl;

    public GameObject attackMove;

    private Pusher()
    {
        health = 15;
        point = 3;
    }

    // Update is called once per frame
    void Update()
    {

        ac = gameObject.GetComponent<animationController>();
        EnemyLogic();
        if (Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
        if ((player.transform.position.x - transform.position.x) <= 0)//turn the object's direction based on where the player is
        {
            transform.eulerAngles = Vector3.forward * 0;
        }
        else
        {

            transform.eulerAngles = Vector3.up * 180;
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public override void minusHealth(int damage)
    {
        //hitSound.Play();
        health -= damage;
        if (health <= (health / 2) && !rageMode)//Enter rage mode if below 50 persent health
        {
            speed *= 1.5f;
            enemyDamage *= 2;
            rageMode = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyState = state.attack;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyState = state.run;

        }
    }
    private void EnemyLogic()
    {
        switch (enemyState)
        {
            case state.run:
                if (playerObj.returnHidingFlag())
                {
                    enemyState = state.idle;
                }
                else
                {
                    enemyState = state.run;
                    ac.PlayStateAnimation("run");
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, player.transform.position.y), speed * Time.deltaTime);
                }
                break;
            case state.attack:
                ac.PlayStateAnimation("attack");
                break;
            case state.idle:
                if (!playerObj.returnHidingFlag())
                {
                    enemyState = state.run;
                }
                else
                {
                    ac.PlayStateAnimation("idle");

                }
                break;
            default:
                break;
        }

    }


}