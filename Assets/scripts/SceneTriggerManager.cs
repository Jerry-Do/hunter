using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneTriggerManager : MonoBehaviour
{
    public static SceneTriggerManager instance { get; private set; }
    private bool storeTrigger = false;
    public bool StoreTrigger { get { return storeTrigger; } set { storeTrigger = value; } }

    private bool cemeteryTrigger = false;
    public bool CemeteryTrigger { get { return cemeteryTrigger; } set { cemeteryTrigger = value; } }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        
    }
}
