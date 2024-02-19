using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie : enemy
{
    // Start is called before the first frame update

    private AudioSource hitSound;
    private GameObject player;
    public Animator sprite;
    //private int score = 1;
    public int health = 10;
    public int enemyDamage = 3;
    [SerializeField] private float speed = 3.0f;
    private playerControl playerControl;
    public bool change = false;
    void Start()
    {
        //hitSound = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<playerControl>();
        //Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            sprite.SetBool("Dead", true);
            Destroy(gameObject);

        }
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance == 6)
        {
            speed *= 2;
        }
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, player.transform.position.y), speed * Time.deltaTime);
    }
    public override void minusHealth(int damage)
    {
        //hitSound.Play();
        sprite.SetBool("Hit", true);
       
        StartCoroutine(cooldown());
        health -= damage;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            playerControl.minusHealth(enemyDamage);
        }
    }
    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(0.001f);
        sprite.SetBool("Hit", false);
    }
}
