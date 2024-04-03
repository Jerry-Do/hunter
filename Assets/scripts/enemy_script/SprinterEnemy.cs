using UnityEngine;

public class SprinterEnemy : enemy
{
    [SerializeField] private float enhancedSpeed = 5f;
    [SerializeField] private float dodgeInterval = 3f;
    [SerializeField] private float dodgeDistance = 2f;
    private float dodgeTimer;

    // Initialization
    private SprinterEnemy()
    {
        health = 3;
        point = 1;
        enemyDamage = 1;
    }

    private void Update()
    {
        MoveTowardsPlayer();

        // Dodging mechanism
        dodgeTimer += Time.deltaTime;
        if (dodgeTimer >= dodgeInterval)
        {
            Dodge();
            dodgeTimer = 0;
        }
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, enhancedSpeed * Time.deltaTime);
    }

    private void Dodge()
    {
        // Example dodge: move perpendicular to the vector towards the player
        Vector2 toPlayer = player.position - transform.position;
        Vector2 dodgeDirection = new Vector2(toPlayer.y, -toPlayer.x).normalized;

        // Randomize dodge direction (left or right)
        dodgeDirection *= Random.Range(0, 2) * 2 - 1;

        transform.position += (Vector3)dodgeDirection * dodgeDistance;
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