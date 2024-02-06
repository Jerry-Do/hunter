using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class store : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject storeUI;
    public Text costHealth;
    public Text costDamage;
    public Text costAmmo;
    public logicManager logic;
    private playerControl player;
    public GameObject spear;
    public int cost = 10;
    public spearSpawn spearScript;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<logicManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerControl>();
        spearScript = spear.GetComponent<spearSpawn>();
    }
 
}
