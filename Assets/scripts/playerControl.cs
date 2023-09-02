using Mono.Cecil;
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

public class playerControl : MonoBehaviour
{
    // Start is called before the first frame update
    
    private weapon weapon;
    private Action shootingFunction;
    private AudioSource sound;
    public DefaultInputActions DefaultInputActions;
    private InputAction move;
    private InputAction shoot;
    public Transform shooter;
    public GameObject reloading;
    private SpriteRenderer gunSprite;
    public GameObject gun;
    public int score;
    public string weaponName;
    private float leftBoundry = 0.03f;
    private float rightBoundry = 4.765f;
    private float upperBoundry = 3.325f;
    private float lowerBoundry = 0.044f;
    private int health = 10;
    private int ammo = 0;
    private int maxNumAmmo = 0;
    private float reloadTimer = 0;
    private float speed = 2;
    private float timer = 0;
    private int money = 0;
    private bool shootFlag = true;
    Vector3 moveDirection;
    private void Awake()
    {
        DefaultInputActions = new DefaultInputActions();
   
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

        gunSprite = gun.GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float rotateY = transform.rotation.y;
        
        if(ammo == 0 && weaponName != "")
        {
            reloading.SetActive(true);
            StartCoroutine(ReloadTime());
            
        }
        if(shootFlag == false)
        {
            StartCoroutine(cooldown());
            
        }
        //if(health == 0)
        //{
        //    Destroy(gameObject);
        //}
        if (transform.position.x < leftBoundry)
        {
            transform.position = new Vector2(leftBoundry, transform.position.y);
        }
        if (transform.position.x > rightBoundry)
        {
            transform.position = new Vector2(rightBoundry, transform.position.y);
        }
        if (transform.position.y > upperBoundry)
        {
            transform.position = new Vector2(transform.position.x, upperBoundry);
        }
        if (transform.position.y < lowerBoundry)
        {
            transform.position = new Vector2(transform.position.x, lowerBoundry);
        }

    }
    private void FixedUpdate()
    {
        moveDirection = move.ReadValue<Vector2>();
        transform.position += moveDirection * speed * Time.fixedDeltaTime;
    }
    private void Fire(InputAction.CallbackContext context)
    {
        if (shootFlag && ammo > 0)
        {
            sound.Play();
            shootingFunction();
            shootFlag = false;
            ammo--;
            timer = 0.5f;
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
        if (collision.gameObject.CompareTag("weapon"))
        {
            weapon = collision.gameObject.GetComponent(collision.gameObject.name) as weapon;
            gunSprite.sprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
            ammo = weapon.returnMaxNumAmmo();
            weaponName = weapon.returnName();
            shootingFunction = weapon.shooting;
            maxNumAmmo = weapon.returnMaxNumAmmo();
            speed = weapon.returnSpeed();
            timer = weapon.returnTimer();
            reloadTimer = weapon.returnReloadTimer();
            Destroy(collision.gameObject);    
        }
    }

    public void addScore(int amount)
    {
        score += amount;
    }
    IEnumerator ReloadTime()
    {
        yield return new WaitForSeconds(reloadTimer);
        ammo = maxNumAmmo;
        reloading.SetActive(false);
    }
    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(timer);
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
}
