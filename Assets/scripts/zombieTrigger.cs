using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class zombieTrigger : MonoBehaviour
{
    public GameObject[] zombie;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        for(int i = 0; i < zombie.Length; i++)
        {
            zombie[i].SetActive(true);
        }
        Destroy(gameObject);
    }
}
