using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomerang : weapon
{
    public bomerang()
    {
        weaponName = "Bomerang";
        reloadTimer = 0f;
        rateOfFire = 99f;
        maxNumAmmo = 1;
        damage = 4;
        rarity = "c";
    }
    private playerControl playerControl;
    public GameObject ammo;
    //public handGun instance;
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<playerControl>();
            Destroy(gameObject);

        }

    }
    // Update is called once per frame
    public override void shooting(bool speedingFlag)
    {

        Instantiate(ammo, new Vector2(playerControl.shooter.transform.position.x, playerControl.shooter.transform.position.y), Quaternion.identity);

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
