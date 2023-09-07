using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class rotation : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    private rotation instance;
    // Start is called before the first frame update
    void Start()
    {
       
        
    }
    // Update is called once per frame
    void Update()
    {

        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        
    }
}

