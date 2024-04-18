using System.Collections;
using UnityEngine;

public class SniperEnemy : enemy
{
    private enum state
    {
        run,
        attack_magic
    }


    private SniperEnemy()
    {
        health = 10;
        point = 1;

    }

    public GameObject laserDevice;
    
    public GameObject bulletPrefab;
    public Transform firePoint;
    private float speed = 3.0f;
    private float stopDistance = 5f;
    private float fireRate = 0.5f;
    private float shootingRange = 15f;
    private float nextFireTime;
    private Laser laserSight;



    private state enemyState;
    public Animator sprite; 
    private animationController ac; 




    void Update()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;







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

        // Check for shooting or moving.
        if (Vector2.Distance(transform.position, player.position) <= shootingRange && Time.time >= nextFireTime)
        {
            
            
            enemyState = state.attack_magic;

        }
        else if (Vector2.Distance(transform.position, player.position) > shootingRange)
        {
            enemyState = state.run;
        }
    }

    void UpdateDirection()
    {
        // Simple direction update logic, flip sprite or model based on player position
        if (player.transform.position.x - transform.position.x <= 0)
        {
            // Enemy faces left
            transform.eulerAngles = Vector3.forward * 0;
        }
        else
        {
            // Enemy faces right
            transform.eulerAngles = Vector3.up * 180;
        }
    }
    void DoDelayAction(float delayTime)
    {
        StartCoroutine(DelayAction(delayTime));
    }

    IEnumerator DelayAction(float delayTime)
    {
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);

        //Do the action after the delay time has finished.
    }
    void Shoot()
    {

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Vector2 shootDirection = (player.position - firePoint.position).normalized;


        EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
        if (bulletScript != null)
        {

            bulletScript.Initialize(shootDirection);
        }


        nextFireTime = Time.time + 1f / fireRate;
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
                MoveTowardsPlayer();
                break;
            case state.attack_magic:
                if (Time.time >= nextFireTime)
                {




                    ac.PlayStateAnimation("attack_magic");
                    Shoot();
                    
                }
                break;
        }
    }

    void MoveTowardsPlayer()
    {
        if (Vector2.Distance(transform.position, player.position) > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }


}
