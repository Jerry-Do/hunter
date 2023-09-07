using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepCamera : MonoBehaviour
{

    public Camera cam;
    private keepCamera keptCamera;
    public CinemachineVirtualCamera vCamera;
    private GameObject player;
    private void Awake()
    {
        if(keptCamera != null)
        {
            Destroy(keptCamera);
            return;
        }
        keptCamera = this;
        DontDestroyOnLoad(keptCamera);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        vCamera.Follow = player.transform;
    }
}
