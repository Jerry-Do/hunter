using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*This is used to turn sprite based on the direction that the player presses
 Can not put this script on the parent object since weird things will happens*/
public class rotateSprite : MonoBehaviour
{
    // Start is called before the first frame update
    private playerControl player;
    public Animator animator;
    void Start()
    {
        player = gameObject.GetComponentInParent<playerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.eulerAngles = Vector3.up * 180;

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.eulerAngles = Vector3.forward * 0;
        }
        
    }
    public void SetSpeed(float speed)
    {
        animator.SetFloat("speed", speed);
    }

    public void SetDeath()
    {
        animator.SetBool("deathFlag", true);
    }
}
