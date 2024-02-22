//using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class playerControl : MonoBehaviour
{
    // Start is called before the first frame update
    private playerControl instance;
    private weapon weapon;
    private Action shootingFunction;
    private AudioSource sound;
    public DefaultInputActions DefaultInputActions;
    private InputAction move;
    private InputAction shoot;
    [SerializeField] private InputActionReference interact;
    public Transform shooter;
    private logicManager logic;
    private SpriteRenderer gunSprite;
    public GameObject gun;
    private rotateSprite sprite;
    private bool speedingFlag;
    [SerializeField] private Rigidbody2D rb;



    [Header("PlayerStats")]

    [SerializeField] private float maxHealth = 10.0f;
    [SerializeField] private float maxFuel = 100.0f;
    [SerializeField] private float speedFuel = 100.00f;
    [SerializeField] private float dashCounter = 0.0f;
    [SerializeField] private float dashLength = 1f;
    [SerializeField] private float dashCooldown = 3.0f;
    [SerializeField] private float dashCoolCounter = 0.0f;
    [SerializeField] private int health = 10;
    [SerializeField]private float speed = 10;




    private string weaponName;
    
    private int ammo = -1;
    private int maxNumAmmo = 0;
    private float reloadTimer = 0;
    
    private float rateOfFire = 0;
    private int money = 0;
    private bool shootFlag = true;
    Vector3 moveDirection;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
            return;
        }
        DefaultInputActions = new DefaultInputActions();
        instance = this;
        DontDestroyOnLoad(instance);
    }
    private void OnEnable()
    {
        move = DefaultInputActions.Player.Move;
        move.Enable();
        shoot = DefaultInputActions.Player.Fire;
        shoot.Enable();
        shoot.performed += Fire;

    }
    private void OnDisable()
    {
        move.Disable();
        shoot.Disable();
    }
    void Start()
    {
        sprite = gameObject.GetComponentInChildren<rotateSprite>();
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<logicManager>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {        
        if(ammo == 0 && weaponName != "")
        {
            
            StartCoroutine(ReloadTime());
           
        }
        if(shootFlag == false)
        {
            StartCoroutine(cooldown());
            
        }
        if(health == 0)
        {
            sprite.SetDeath();
        }
    }
    private void FixedUpdate()
    {
        //Moving Character
        float speedBoost = 1;
        speedingFlag = false;
        moveDirection = move.ReadValue<Vector2>();
        moveDirection.Normalize();
        //transform.position += moveDirection * activeSpeed * Time.fixedDeltaTime;
        



        if (Input.GetKey(KeyCode.Space))//sprint
        {
            speedingFlag = true;
            if (speedFuel > 0)
            {
                speedBoost = 1.5f;
                speedFuel -= Time.fixedDeltaTime * 20;
            }
            if(speedFuel < 0)
            {
                speedFuel = 0;
                speedBoost = 1;
            }
        }
        if(speedFuel < 100)
        {
            speedFuel += Time.fixedDeltaTime * 10;
        }
        if(Input.GetKey(KeyCode.LeftShift) && speedingFlag == true)//dash
        {
           if(dashCoolCounter <= 0 && dashCounter <= 0)
            {
                speedBoost *= 10;
                dashCounter = dashLength;
            }
          
        }
        if(dashCounter > 0)
        {
            Debug.Log("Dashing");
            dashCounter -= Time.fixedDeltaTime;
            if(dashCounter <= 0)
            {
                speedBoost = 1;
                dashCoolCounter = dashCooldown;
            }

        }

        if(dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.fixedDeltaTime; 
        }
       
       
        rb.velocity = moveDirection * speed * speedBoost;
        
        sprite.SetSpeed(Mathf.Abs(Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.y)));//set the speed of the sprite animation
        
    }
    private void Fire(InputAction.CallbackContext context)
    {
        if (shootFlag && ammo > 0)
        {
            sound.Play();
            weapon.shooting(speedingFlag);
            shootFlag = false;
            ammo--;
            
        }
    }

    public void minusHealth(int damage)
    {
        health -= damage;
    }

    public void increaseMoney(int amount)
    {
        money += amount;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("weapon"))//Pick up weapon
        {
            gunSprite = gun.GetComponent<SpriteRenderer>();
            weapon = collision.gameObject.GetComponent<weapon>();
            gunSprite.sprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
            ammo = weapon.returnMaxNumAmmo();
            weaponName = weapon.returnName();
             
            maxNumAmmo = weapon.returnMaxNumAmmo();
            reloadTimer = weapon.returnReloadTimer();
            Destroy(collision.gameObject);    
        }
        if(collision.gameObject.CompareTag("obsticle"))
        {
            Debug.Log("Collided");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("door"))
        {
            logic.LoadScene(collision.gameObject.name);
        }
    }


    public void addScore(int amount)
    {

        if (health < maxHealth)
        {
            health += amount;
        }
    }
    public void plusFuel(int amount)
    {
        if (speedFuel < maxFuel)
        {
            speedFuel += amount;
        }
    }
  

    IEnumerator ReloadTime()
    {

        yield return new WaitForSeconds(reloadTimer);
        ammo = maxNumAmmo;
        
    }
    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(weapon.returnRateOfFire());
        shootFlag = true;
    }
   
    public int ReturnMoney()
    {
        return money;
    }

    public int ReturnMaxAmmo()
    {
        return maxNumAmmo;
    }

    public int ReturnHealth()
    {
        return health;
    }

    public int ReturnCurrentAmmo()
    {
        return ammo;
    }

    public float ReturnReloadTime()
    {
        return reloadTimer;
    }

    public float ReturnSpeed()
    {
        return speed;
    }

    public float ReturnFuel()
    {
        return speedFuel;
    }
}
