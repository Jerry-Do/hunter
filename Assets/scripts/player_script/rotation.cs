using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Script for rotating the gun
public class rotation : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    private rotation instance;
    [SerializeField] private SpriteRenderer sprite;
    public gameStateManager gameStateManager;
    // Start is called before the first frame update
    void Start()
    {
        gameStateManager = FindObjectOfType<gameStateManager>();

    }
    // Update is called once per frame
    void Update()
    {
        if (!gameStateManager.IsGamePaused)
        {
            mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

            Vector3 rotation = mousePos - transform.position;

            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }
    }
}

