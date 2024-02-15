using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBarController : MonoBehaviour
{
    [SerializeField] private Slider HealthBar;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GameObject.FindGameObjectWithTag("Player").GetComponent<playerControl>().ReturnHealth());
        HealthBar.value = GameObject.FindGameObjectWithTag("Player").GetComponent<playerControl>().ReturnHealth();
    }
}
