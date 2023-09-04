using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepCamera : MonoBehaviour
{

    public Camera cam;
    private keepCamera keptCamera;
    private void Awake()
    {
        keptCamera = this;
        DontDestroyOnLoad(keptCamera);
    }
}
