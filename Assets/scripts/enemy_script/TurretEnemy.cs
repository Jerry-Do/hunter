using UnityEngine;

public class TurretEnemy : enemy
{
    public Transform turretHead;
    public GameObject bulletPrefab;
    public Transform shootingPoint;

    [SerializeField] private float fireRate = 1f;
    private float nextFireTime = 0f;

    // Constants for better configurability and readability
    private const float AngleAdjustment = -90f;
    private const float RotationSpeed = 10f;

    private void Update()
    {
        RotateTurret();

        if (Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void RotateTurret()
    {
        Vector2 direction = player.position - turretHead.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + AngleAdjustment;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        turretHead.rotation = Quaternion.Slerp(turretHead.rotation, targetRotation, Time.deltaTime * RotationSpeed);
    }

    void Shoot()
    {
        // Explicitly create a rotation quaternion for clarity
        Quaternion bulletRotation = Quaternion.Euler(0, 0, turretHead.eulerAngles.z);
        Instantiate(bulletPrefab, shootingPoint.position, bulletRotation);
    }

    public override void minusHealth(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}