using UnityEngine;

public class HealerEnemy : enemy
{
    [SerializeField] private float healRange = 5f;
    [SerializeField] private int healAmount = 2;
    [SerializeField] private float healCooldown = 3f;
    private float nextHealTime;

    public HealerEnemy()
    {
        health = 12; // Example health value
        point = 3; // Example score value upon defeat
        enemyDamage = 0; // Healers don't directly damage
    }

    private void Update()
    {
        if (Time.time > nextHealTime)
        {
            HealAllies();
            nextHealTime = Time.time + healCooldown;
        }
    }

    void HealAllies()
    {
        enemy[] enemies = FindObjectsOfType<enemy>();
        foreach (var otherEnemy in enemies)
        {
            if (otherEnemy != this && Vector2.Distance(transform.position, otherEnemy.transform.position) <= healRange)
            {
         //       otherEnemy.Heal(healAmount);
            }
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

    public void Heal(int amount)
    {
        health += amount;
    }
}