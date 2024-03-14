using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class richocetGun : weapon
{
    public richocetGun()
    {
        weaponName = "richocet gun";
        reloadTimer = 1.5f;
        rateOfFire = 2f;
        maxNumAmmo = 10;
        damage = 0;
        rarity = "r";
    }

    private playerControl playerControl;
    public GameObject ammo;
    public richocetGun instance;
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
        float randomAngle = UnityEngine.Random.Range(-5, 5);
        Instantiate(ammo, playerControl.shooter.transform.position, (speedingFlag == true ? Quaternion.Euler(new Vector3(randomAngle, 0, 0)) : Quaternion.identity));

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
