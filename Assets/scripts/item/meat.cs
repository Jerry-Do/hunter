using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meat : Item
{
    // Start is called before the first frame update
    private meat()
    {
        itemName = "Meat";
        description = "Heal player for an amount";
        unlocked = false;
        times_picked = 0;
    }
    public override void pick(playerControl player)
    {
        player.plusHealth(1);
    }
    public override string returnName()
    {
        return itemName;
    }
    public override string getDescription()
    {
        return description;
    }
    public override bool returnUnlocked()
    {
        return unlocked;
    }
    
    public override int returnTimesPicked()
    {
        return times_picked;
    }
}
