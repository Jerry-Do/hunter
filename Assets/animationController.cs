using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationController : MonoBehaviour
{
  
    public SPUM_Prefabs _prefabs;
    public void PlayStateAnimation(string state)
    {
        _prefabs.PlayAnimation(state);
    }
}
