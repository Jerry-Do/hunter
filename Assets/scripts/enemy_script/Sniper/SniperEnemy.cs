using UnityEngine;

public class SniperEnemy : enemy
{
    private enum State
    {
        Run,
        AttackMagic
    }

    private SniperEnemy()
    {
        health = 10;
        point = 1;

    }


    public GameObject bulletPrefab;
    public Transform firePoint;
 private float speed = 3.0f;
private float stopDistance = 5f;
 private float fireRate = 2f;
private float shootingRange = 15f;
    private float nextFireTime;




    private State enemyState;
    public Animator sprite; 
    private animationController ac; 


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            return;
        }

        // Update the direction to face the player
        UpdateDirection();

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= shootingRange && Time.time >= nextFireTime)
        {
            enemyState = State.AttackMagic;
        }
        else if (distanceToPlayer > stopDistance)
        {
            enemyState = State.Run;
        }

        EnemyLogic();
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

    private void EnemyLogic()
    {
        switch (enemyState)
        {
            case State.Run:
                ac.PlayStateAnimation("Run");
                MoveTowardsPlayer();
                break;
            case State.AttackMagic:
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

    public override void minusHealth(int damage) // Adjusted method name to match C# naming conventions
    {
        health -= damage;
        
    }
}
