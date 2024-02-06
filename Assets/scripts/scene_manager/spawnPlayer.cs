using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
        {
            Instantiate(player, transform.position, Quaternion.identity);
        }
    }
}
