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
        speed = 10;
        
    }
 
    
    /*
     * TODO:
     * Make a speical case for the reloading of the bomerang where the ammo would not go up until the bomerang hit the player
     * make sprite for the gun disappear until the bomerang hit the player.
     */
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
