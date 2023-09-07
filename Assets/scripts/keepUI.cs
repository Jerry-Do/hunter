using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepUI : MonoBehaviour
{
    // Start is called before the first frame update
    private keepUI instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
            return;
        }
        instance = this;
        DontDestroyOnLoad(instance);
    }
}
