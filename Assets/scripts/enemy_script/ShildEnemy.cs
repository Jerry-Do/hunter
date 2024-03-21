using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : enemy
{
    public GameObject shieldPrefab; // Prefab for the shield object
    public Transform shieldSocket; // Socket where the shield will be attached
    public float rotationSpeed = 5f; // Speed at which the shield rotates towards the player
    public float speed = 2f; // Speed of the enemy movement

    private GameObject shieldInstance; // Reference to the spawned shield object
    private Shield shield; // Reference to the Shield component of the shield object

       void Start()
    {
        

        // Instantiate the shield and attach it to the shield socket
        if (shieldPrefab != null && shieldSocket != null)
        {
            shieldInstance = Instantiate(shieldPrefab, shieldSocket.position, Quaternion.identity, shieldSocket);
            shield = shieldInstance.GetComponent<Shield>(); // Get the Shield component
        }
    }

       void Update()
    {
        

        // Rotate the shield towards the player
        RotateShield();

        // Move the enemy towards the player
        MoveTowardsPlayer();
    }

    void RotateShield()
    {
        if (player != null)
        {
            // Calculate the direction to the player
            Vector3 direction = player.position - shieldSocket.position;
            direction.z = 0f;

            // Calculate the target rotation based on the direction
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);

            // Smoothly rotate the shield towards the player
            shieldSocket.rotation = Quaternion.Slerp(shieldSocket.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            // Calculate the direction to the player
            Vector3 direction = player.position - transform.position;

            // Move the enemy towards the player
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
    }

    // Method to take damage to the shield
    public override void minusHealth(int damage)
    {
        if (shield != null)
        {
            shield.TakeDamage(damage); // Call the TakeDamage method of the shield
        }
    }
}
