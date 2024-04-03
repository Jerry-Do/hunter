using UnityEngine;

public class SniperEnemy : enemy
{
    public LineRenderer laserSight;
    public GameObject bulletPrefab;
    public Transform shootingPoint;
    [SerializeField] private float aimingTime = 2.0f;
    [SerializeField] private float shootingRange = 10.0f;
    private float aimingTimer;

    // SniperEnemy constructor
    public SniperEnemy()
    {
        health = 15;
        point = 3;
        enemyDamage = 4;
    }

    private void Update()
    {
        AimAndShoot();
    }

    void AimAndShoot()
    {
        if (Vector2.Distance(transform.position, player.position) <= shootingRange)
        {
            if (aimingTimer < aimingTime)
            {
                laserSight.enabled = true;
                laserSight.SetPosition(0, shootingPoint.position);
                laserSight.SetPosition(1, player.position);
                aimingTimer += Time.deltaTime;
            }
            else
            {
                Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
                aimingTimer = 0f;
                laserSight.enabled = false;
            }
        }
        else
        {
            aimingTimer = 0f;
            laserSight.enabled = false;
        }
    }

    public override void minusHealth(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject); // Destroy on death
        }
    }
}