using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotGun : weapon
{
    public shotGun()
    {
        weaponName = "Shot Gun";
        reloadTimer = 3f;
        rateOfFire = 1.0f;
        maxNumAmmo = 5;
        damage = 3;
        rarity = "sr";
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
        Instantiate(ammo, playerControl.shooter.transform.position, Quaternion.identity);
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
