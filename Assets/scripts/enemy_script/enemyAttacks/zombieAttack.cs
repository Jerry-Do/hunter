using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieAttack : enenmyAttack
{
    [SerializeField] private int damage;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            playerControl pc = FindObjectOfType<playerControl>();
            pc.minusHealth(damage);
        }
    }
}
