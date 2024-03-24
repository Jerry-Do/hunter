using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    public float lifespan = 2f;

    private Rigidbody2D rb;
    private float startTime;
    private Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Calculate the direction the bullet should move based on its rotation
        direction = transform.up;

        // Set the velocity of the bullet
        rb.velocity = direction * speed;

        // Start tracking the time the bullet was created
        startTime = Time.time;
    }

    void Update()
    {
        // Check if the bullet has exceeded its lifespan
        if (Time.time - startTime >= lifespan)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle collision with player
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            playerControl player = collision.gameObject.GetComponent<playerControl>();
            if (player != null)
            {
                player.minusHealth(damage);
            }
        }




        // Destroy the bullet upon collision with an obstacle or another bullet
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }



}

