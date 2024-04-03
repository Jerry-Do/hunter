using UnityEngine;

public class JumperEnemy : enemy
{
    [SerializeField] private float jumpForce = 700f;
    [SerializeField] private float cooldownTime = 2f;
    private Rigidbody2D rb;
    private float nextJumpTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Time.time >= nextJumpTime)
        {
            JumpTowardsPlayer();
            RotateTowardsPlayer();
            nextJumpTime = Time.time + cooldownTime;
        }
    }

    void JumpTowardsPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized * jumpForce;
            rb.AddForce(new Vector2(direction.x, Mathf.Abs(direction.y) * 2.5f)); // Adjust y component for a better jumping effect
        }
    }

    void RotateTowardsPlayer()
    {
        transform.eulerAngles = player.transform.position.x < transform.position.x ? Vector3.forward * 0 : Vector3.up * 180;
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