using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemy;
    private enemyScript enemyScript;
    private float spawnRate = 5;
    private float timer = 0;
    void Start()
    {
        enemyScript = enemy.GetComponent<enemyScript>();
    }

    // Update is called once per frame
    void Update()
    {

        if(timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Instantiate(enemy,new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            timer = UnityEngine.Random.Range(0,3); 
        }
    }

    public void upgradeMob()
    {
        enemyScript.health += 1;
        enemyScript.enemyDamage += 1;
        spawnRate--;
    }
}
