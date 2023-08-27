using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class weapon: MonoBehaviour
{
    protected string weaponName;
    protected float reloadTimer;
    protected float speed;
    protected float timer;
    protected int maxNumAmmo;
    
    public abstract void shooting();
    public abstract string returnName();
    public abstract float returnReloadTimer();
    public abstract float returnSpeed();
    public abstract float returnTimer();
    public abstract int returnMaxNumAmmo();
}
