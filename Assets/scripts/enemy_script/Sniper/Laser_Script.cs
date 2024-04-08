using UnityEngine;

public class LaserSight : MonoBehaviour
{
    public Transform firePoint; // The firing point of the sniper's rifle
    public Transform target; // Public to allow assignment directly or from another script
    private LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false; // Initially disabled, enable when preparing to shoot
    }

    void Update()
    {
        // Ensure the laser sight is pointing from the firePoint to the target
        if (target != null)
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, target.position);
        }
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