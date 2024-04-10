using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
     
    private LineRenderer lineRenderer;
    public Transform LaserPoint;


    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false; // Initially disabled, enable when preparing to shoot
        lineRenderer.useWorldSpace = true;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
    }   

    void Update()
    {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right);
        
            LaserPoint.position = hit.point;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, LaserPoint.position);

            

    }

    // Call these methods from the sniper enemy script when appropriate
    public void ActivateLaserSight()
    {
        lineRenderer.enabled = true;
    }

    public void DeactivateLaserSight()
    {
        lineRenderer.enabled = false;
    }
}