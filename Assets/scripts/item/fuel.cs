using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class fuel : Item
{
    // Start is called before the first frame update
    private fuel()
    {
        itemName = "Fuel";
        description = "Increase player's fuel";
        unlocked = false;
        times_picked = 0;
    }
    public override void pick(playerControl player)
    {
        player.plusFuel(1);
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
