using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : enemy
{
    public GameObject shieldPrefab; // Prefab for the shield object
    public Transform shieldSocket; // Socket where the shield will be attached
    public float rotationSpeed = 5f; // Speed at which the shield rotates
    public float speed = 2f; // Speed of the enemy movement
    private GameObject shieldInstance; // Reference to the spawned shield object
    private Shield shield; // Reference to the Shield component of the shield object
    void Start()
    {

        // Instantiate the shield and attach it to the shield socket
        if (shieldPrefab != null && shieldSocket != null)
        {
            shieldInstance = Instantiate(shieldPrefab, shieldSocket.position, Quaternion.identity, shieldSocket);
            shield = shieldInstance.GetComponent<Shield>(); // Get the Shield component to allow interaction with shield-specific methods
        }
    }
    void Update()
    {
        RotateShield();
        MoveTowardsPlayer();
    }
    void RotateShield()
    {
        if (player != null && shieldSocket != null)
        {
            // Calculate the direction to the player
            Vector3 direction = player.position - shieldSocket.position;
            direction.z = 0f;
            // Calculate the target rotation so the shield always faces the player
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
            // Apply the rotation to the shield socket, making the shield face the player
            shieldSocket.rotation = Quaternion.Slerp(shieldSocket.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            // Calculate the direction to the player
            Vector3 direction = player.position - transform.position;
            // Normalize the direction and move the enemy towards the player
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
    }
    public override void minusHealth(int damage)
    {
        // Override the minusHealth method to delegate the damage to the shield component if it exists
        if (shield != null)
        {
            // Since Shield is now supposed to inherit from enemy and use minusHealth,
            // ensure this calls minusHealth instead of TakeDamage.
            shield.minusHealth(damage); // Call the minusHealth method on the shield component
        }
    }
}