using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Shield : enemy // Inherit from enemy
{
    public int maxHealth = 50; // This might be redundant now since `health` is inherited from `enemy`

    void Start()
    {
        health = maxHealth; // Initialize current health to maximum health, use `health` from `enemy`
    }
    public override void minusHealth(int damage) // Implementation of the abstract method `minusHealth`
    {
        health -= damage; // Deduct damage from health
        // Check if the shield (now acting as an enemy) is destroyed
        if (health <= 0)
        {
            Destroy(gameObject); // Destroy the shield object
        }
    }
}