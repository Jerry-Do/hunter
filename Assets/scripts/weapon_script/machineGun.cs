using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class machineGun : weapon
{
    public machineGun()
    {
        weaponName = "Machine Gun";
        reloadTimer = 4f;
        rateOfFire = 0.1f;
        maxNumAmmo = 30;
        damage = 1;
        rarity = "r";
    }

    private playerControl playerControl;
    public GameObject ammo;
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<playerControl>();
        }

    }
    // Update is called once per frame
    public override void shooting(bool speedingFlag)
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
