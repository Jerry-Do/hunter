using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotGun : weapon
{
    public shotGun()
    {
        weaponName = "Shot Gun";
        reloadTimer = 3f;
        speed = 2.2f;
        timer = 1.0f;
        maxNumAmmo = 5;
    }

    private playerControl playerControl;
    public GameObject ammo;
    // Start is called before the first frame update
    void Start()
    {

        playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<playerControl>();
    }

    // Update is called once per frame
    public override void shooting()
    {
        
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

    public override float returnSpeed()
    {
        return speed;
    }

    public override float returnTimer()
    {
        return timer;
    }

    public override int returnMaxNumAmmo()
    {
        return maxNumAmmo;
    }
}
