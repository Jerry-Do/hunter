using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handGun : weapon
{
    public handGun()
    {
        weaponName = "Hand Gun";
        reloadTimer = 1.5f;
        rateOfFire = 2f;
        maxNumAmmo = 10;
        damage = 4;
    }

    private playerControl playerControl;
    public GameObject ammo;
    public handGun instance;
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<playerControl>();
            
        }
        
    }
    // Update is called once per frame
    public override void shooting()
    {
        ammo.GetComponent<spearSpawn>().damage = damage;
        Instantiate(ammo, playerControl.shooter.transform.position, Quaternion.identity);
        
    }

    public override string returnName()
    {
        return weaponName;
    }

    public override float returnReloadTimer()
    {
        return reloadTimer;
    }


    public override float returnRateOfFire()
    {
        return rateOfFire;
    }

    public override int returnMaxNumAmmo()
    {
        return maxNumAmmo;
    }
}
