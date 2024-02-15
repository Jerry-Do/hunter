using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class item : MonoBehaviour
{
    protected string itemName;
    protected string description;
    protected bool unlocked;
    protected int times_picked;

    public abstract void pick();
    public abstract string returnName();
    public abstract string getDescription();
    public abstract bool returnUnlocked();
    public abstract int returnTimesPicked();

}
