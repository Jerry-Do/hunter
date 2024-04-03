using UnityEngine;

public class RangedEnemy : enemy
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    [SerializeField] private float speed = 3.0f;
    public float fireRate = 1f;
    public float shootingRange = 10f;
    public float minDistanceToPlayer = 8f;
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
            return; // Early exit to avoid unnecessary calculations after destruction
        }

        RotateTowardsPlayer();

        if (Time.time >= nextFireTime && Vector2.Distance(transform.position, player.position) <= shootingRange)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }

        if (!IsInShootingRange() && Vector2.Distance(transform.position, player.position) > minDistanceToPlayer)
        {
            MoveTowardsPlayer();
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // Adjust rotation offset as needed
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    bool IsInShootingRange() => Vector2.Distance(transform.position, player.position) <= shootingRange;

    void MoveTowardsPlayer() => transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

    void Shoot() => Instantiate(bulletPrefab, firePoint.position, Quaternion.identity); // Consider the bullet's rotation if needed

    public override void minusHealth(int damage)
    {
        health -= damage;
    }
}