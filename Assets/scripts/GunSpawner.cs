using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpawner : MonoBehaviour
{
    public GameObject gun;
    // Start is called before the first frame update
    void Start()
    {
        if(SceneTriggerManager.instance.StoreTrigger == false)
        {
            Instantiate(gun, transform.position, Quaternion.identity);
            
        }
    }
    private void Update()
    {
        if(GameObject.Find("handGun(Clone)") == null)
        {
            SceneTriggerManager.instance.StoreTrigger = true;
        }
    }
}
