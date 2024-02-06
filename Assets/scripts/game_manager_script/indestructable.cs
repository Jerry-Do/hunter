using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indestructable : MonoBehaviour
{
    public static indestructable instance = null;
    // Start is called before the first frame update
    public int prevScene = -1;
    void Awake()
    {
        // If we don't have an instance set - set it now
        if (!instance)
            instance = this;
        // Otherwise, its a double, we dont need it - destroy
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }
    // Update is called once per frame
 
}
