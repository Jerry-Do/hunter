using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hiding : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(Input.GetMouseButtonDown(0) && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Desri");
            Destroy(gameObject);
        }
    }
}
