using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class homingAmmo : MonoBehaviour
{
    public float speed = 0.5f;
    private GameObject player;
    private playerControl playerControl;
    private int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<playerControl>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, player.transform.position.y), speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            playerControl.minusHealth(damage);
        }
        if(collision.gameObject.CompareTag("Obsticle"))
        {
            Destroy(gameObject);
        }
    }
}
