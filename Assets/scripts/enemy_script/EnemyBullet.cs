using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f; // The speed at which the bullet moves.
    public int damage = 1; // The damage the bullet will deal upon hitting the player.
    public float lifespan = 2f; // How long the bullet exists before being automatically destroyed.

    private Rigidbody2D rb;
    private float creationTime; // To track the bullet's age and destroy it after its lifespan.

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
        creationTime = Time.time; // Record the time of creation.
    }

    public void Initialize(Vector2 direction)
    {
        rb.velocity = direction.normalized * speed; // Set the bullet's velocity in the specified direction.

    
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90; // Adjust rotation to align with direction.
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        // Destroy the bullet if it exceeds its lifespan.
        if (Time.time - creationTime >= lifespan)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // React to collisions with specific tags.
        if (collision.gameObject.CompareTag("Player"))
        {
            // Optionally deal damage to the player.
            playerControl player = collision.gameObject.GetComponent<playerControl>();
            if (player != null)
            {
                player.minusHealth(damage);
            }

            Destroy(gameObject); // Destroy the bullet on collision.
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject); // Destroy the bullet when hitting an obstacle.
        }
        
    }
}

