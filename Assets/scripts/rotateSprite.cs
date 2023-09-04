using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
