using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class bomerangBullet : bullet
{
    // Start is called before the first frame update
    [SerializeField] private float returnTimer;
    private bool returnFlag = false;
    public bomerangBullet()
    {
        speed = 15;
        
    }
 
    
    void Update()
    {
        returnTimer -= Time.deltaTime;
        if(returnTimer <= 0)
        {
            rb2D.velocity = Vector3.zero;
            Vector2 playerPos = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y).normalized * speed * 2;
            rb2D.velocity = playerPos;
        }
        transform.Rotate(0, 0, 15);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb2D.velocity = Vector2.zero;
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("enemy"))
        {
            enemy enemyObject = collision.gameObject.GetComponent<enemy>();
            enemyObject.minusHealth(damage);
            returnTimer = 0;
        }
        if (collision.gameObject.CompareTag("obsticle"))
        {
            returnTimer = 0;
        }
    }
}
