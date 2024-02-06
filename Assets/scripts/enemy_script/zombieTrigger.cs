using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class zombieTrigger : MonoBehaviour
{
    public GameObject[] zombie;
    public GameObject shotGun;
    private bool flag = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SceneTriggerManager.instance.CemeteryTrigger == false)
        {
            for (int i = 0; i < zombie.Length; i++)
            {
                zombie[i].SetActive(true);
            }
            flag = true;
        }
    }

    private void Update()
    {
        if(GameObject.FindGameObjectsWithTag("enemy").Length == 0 && flag == true)
        {
            shotGun.SetActive(true);
            Destroy(gameObject);
            SceneTriggerManager.instance.CemeteryTrigger = true;
        }
        
    }
}
