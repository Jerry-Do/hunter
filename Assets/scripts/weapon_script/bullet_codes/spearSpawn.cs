using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO: make the spear go in the direction where the character is facing
public class spearSpawn : MonoBehaviour
{
    public float speed = 12.0f;
    public int damage = 5;
    private GameObject player;
    private Vector3 mousePos;
    private Camera mainCam;
    public Rigidbody2D spear;
    public Vector2 test;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //spear.velocity = transform.right * speed;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        spear.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if(dist > 75.0f)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("obsticle"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
           
            enemy enemyObject = collision.gameObject.GetComponent<enemy>();
            enemyObject.minusHealth(damage);
            Destroy(gameObject);
        }
    }
}
