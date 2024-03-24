using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// Keep track of metadata like point, point multiplier, enemy killed and weapons picked up
/// </summary>
public class dataTracker : MonoBehaviour
{
    // Start is called before the first frame update
    private logicManager lm;
    private double point;
    private double pointMultiplier;
    private int enemyKilled;
    private List<string> weaponNames;
    void Start()
    {
        lm = FindObjectOfType<logicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void getData()//this function is called when the game ends, move all of this code to Update if you want testing
    {
        point = lm.getPoint();
        pointMultiplier = lm.getMultiplier();
    }
    public void increaseKillCount()
    {
        enemyKilled++;
    }

    public void addWeapon(string name)
    {
        if (!weaponNames.Contains(name))
        {
            weaponNames.Add(name);
        }
    }
}
