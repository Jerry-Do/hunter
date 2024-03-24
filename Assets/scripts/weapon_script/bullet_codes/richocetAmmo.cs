using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;
/// <summary>
/// Logic the richocet ammo
/// </summary>
public class richocetAmmo : bullet
{
    // Start is called before the first frame update
   
    private richocetAmmo()
    {
        speed = 12f;
        damage = 10;
    }


    void Update()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist > 75.0f)
        {
            Destroy(gameObject);
        }
      
    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("obsticle"))
        {
            Vector2 normal = collision.contacts[0].normal;
            Vector2 dir = Vector2.Reflect(rb2D.velocity, normal).normalized;
            rb2D.velocity = dir * speed * 2;
            enemy enemyObject = collision.gameObject.GetComponent<enemy>();
            enemyObject.minusHealth(damage);
        }

    }
    
    
}
