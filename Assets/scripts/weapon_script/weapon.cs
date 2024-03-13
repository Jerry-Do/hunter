using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class weapon: MonoBehaviour
{
    protected string weaponName;
    protected float reloadTimer;
    protected float rateOfFire;
    protected int maxNumAmmo;
    protected int damage;
    protected string rarity;
    public abstract void shooting(bool speedingFlag);
    public abstract string returnName();
    public abstract float returnReloadTimer();
    public abstract float returnRateOfFire();
    public abstract int returnMaxNumAmmo();


}
