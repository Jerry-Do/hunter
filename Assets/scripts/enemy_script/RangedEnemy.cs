using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerObj; // Make sure PlayerObj is correctly defined in your project.

public class RangedEnemy : enemy
{

    private enum state
    {
        run,
        attack_magic
    }

    public GameObject bulletPrefab;
    public Transform firePoint;
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float stopDistance = 5f; // Minimum distance to stop moving and start attacking.
    public float fireRate = 1f;
    public float shootingRange = 10f;
    private float nextFireTime;



    private RangedEnemy()
    {
        health = 10;
        point = 1;
        
    }

    private state enemyState;

    public Animator sprite; // Make sure it's attached in the inspector.
    private animationController ac; // Attach this in the inspector as well.

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

    void Shoot()
    {
        // Instantiate the bullet at the firePoint position, without any rotation.
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Calculate the direction from the enemy to the player, normalized to get a unit vector.
        Vector2 shootDirection = (player.position - firePoint.position).normalized;

        // Attempt to get the EnemyBullet component on the newly instantiated bullet object.
        EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
        if (bulletScript != null)
        {
            // If the component is found, call the Initialize method, passing the calculated direction.
            bulletScript.Initialize(shootDirection);
        }

        // Update the nextFireTime by adding the reciprocal of the fire rate to the current time.
        // This ensures that the enemy will only shoot again after the specified fire rate interval has passed.
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
                if (Vector2.Distance(transform.position, player.transform.position) > stopDistance)
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                break;
            case state.attack_magic:
                ac.PlayStateAnimation("attack_magic"); // Play the attack animation.
                
                if (Time.time >= nextFireTime)
                {
                    Shoot();
                    break;
                }
            
                break;
        }
    }
}