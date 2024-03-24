using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int maxHealth = 50; // Maximum health of the shield
    private int currentHealth; // Current health of the shield

    void Start()
    {
        currentHealth = maxHealth; // Initialize current health to maximum health
    }

    // Method to take damage
    public void TakeDamage(int damage)
    {
        // Reduce health by the amount of damage taken
        currentHealth -= damage;

        // Check if the shield is destroyed
        if (currentHealth <= 0)
        {
            Destroy(gameObject); // Destroy the shield object
        }
    }
}
