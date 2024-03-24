using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


public class RangedEnemy : enemy
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    [SerializeField] private float speed = 3.0f;
    public float fireRate = 1f;
    public float shootingRange = 10f; // Adjust this value as needed
    public float minDistanceToPlayer = 8f; // Adjust this value as needed
    private float nextFireTime;


    private RangedEnemy()
    {
        health = 10;
        point = 1;
        enemyDamage = 5;
    }


    void Update()
    {

        if (health <= 0)
        {
            Destroy(gameObject);

        }

        // Rotate towards the player
        Vector2 direction = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Check if it's time to shoot
        if (Time.time >= nextFireTime && Vector2.Distance(transform.position, player.position) <= shootingRange)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }

        // Check if the enemy should move towards the player
        if (!IsInShootingRange() && Vector2.Distance(transform.position, player.position) > minDistanceToPlayer)
        {
            MoveTowardsPlayer();
        }
    }

    bool IsInShootingRange()
    {
        return Vector2.Distance(transform.position, player.position) <= shootingRange;
    }

    void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    public override void minusHealth(int damage)
    {
        health -= damage;
    }
}
