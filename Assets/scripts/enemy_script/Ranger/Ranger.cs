using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ranger : enemy // Assuming enemy inherits MonoBehaviour
{
    private enum State
    {
        Run,
        AttackMagic
    }
    private Ranger()
    {
        health = 10;
        point = 1;

    }


    public GameObject bulletPrefab;
    public Transform firePoint;
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float stopDistance = 5f;
    public float fireRate = 1f;
    public float shootingRange = 10f;
    private float nextFireTime;
    public int shotgunPellets = 5; // Number of bullets fired in one shotgun blast
    public float spreadAngle = 45f; // Total spread angle of shotgun blast



    private State enemyState;
    public Animator sprite;
    private animationController ac;



    void Update()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;

 

        if (health <= 0)
        {
            Destroy(gameObject);
            return;
        }
        UpdateDirection();

        if (Vector2.Distance(transform.position, player.position) <= shootingRange && Time.time >= nextFireTime)
        {
            enemyState = State.AttackMagic;
        }
        else if (Vector2.Distance(transform.position, player.position) > shootingRange)
        {
            enemyState = State.Run;
        }
    }

    void Shoot()
    {
        for (int i = 0; i < shotgunPellets; i++)
        {
            // Calculate the rotation for each bullet within the spread
            float spread = Random.Range(-spreadAngle / 2, spreadAngle / 2);
            Quaternion bulletRotation = Quaternion.Euler(0, 0, spread) * Quaternion.LookRotation(player.position - firePoint.position);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);

            Vector2 shootDirection = bullet.transform.right; // Use the bullet's right as the direction

            EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
            if (bulletScript != null)
            {
                bulletScript.Initialize(shootDirection);
            }
        }
        nextFireTime = Time.time + 1f / fireRate;
    }

    private void UpdateDirection()
    {
        if (player.transform.position.x - transform.position.x <= 0)
        {
            transform.eulerAngles = Vector3.forward * 0;
        }
        else
        {
            transform.eulerAngles = Vector3.up * 180;
        }
    }

    public override void minusHealth(int damage)
    {
        health -= damage;
        
    }

    private void EnemyLogic()
    {
        switch (enemyState)
        {
            case State.Run:
                ac.PlayStateAnimation("run");
                MoveTowardsPlayer();
                break;
            case State.AttackMagic:
                ac.PlayStateAnimation("attack_magic");
                if (Time.time >= nextFireTime)
                {
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
