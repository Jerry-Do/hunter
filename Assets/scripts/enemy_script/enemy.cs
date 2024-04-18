using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Base class for all the enemies in the game
/// </summary>
public abstract class enemy : MonoBehaviour
{
    [SerializeField] protected float minSeparationDistance = 0f; // Minimum separation distance
    [SerializeField] protected float maxSeparationDistance = 0f; // Maximum separation distance
    [SerializeField] protected float correctionStrength = 0f;  // How strong the correction should be
    [SerializeField] protected float playerProximityThreshold = 3.0f; // Distance within which collision avoidance is disabled

    [SerializeField] protected float despawnDistance = 20f;
    protected Transform player;
    protected playerControl playerObj; 
    protected int point;
    protected int health;
    protected int enemyDamage;
    public enum EnemyType { Melee, Ranged, Healer, Heavy }
    public EnemyType enemyType;


    private void Start()
    {
        player = FindAnyObjectByType<playerControl>().transform;
        playerObj = FindAnyObjectByType<playerControl>();
    }

    void LateUpdate()
    {
        if ((player.position - transform.position).magnitude > playerProximityThreshold)
        {
            PerformSeparation();
        }
    }


    void PerformSeparation()
    {
        float currentSeparationDistance = Random.Range(minSeparationDistance, maxSeparationDistance);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, currentSeparationDistance);
        Vector3 correctionVector = Vector3.zero;
        int count = 0;

        foreach (var hit in hits)
        {
            if (hit != null && hit.gameObject != gameObject && hit.CompareTag("enemy"))
            {
                Vector3 diff = transform.position - hit.transform.position;
                float distance = diff.magnitude;
                if (distance < currentSeparationDistance)
                {
                    correctionVector += diff.normalized * (currentSeparationDistance - distance);
                    count++;
                }
            }
        }

        if (count > 0)
        {
            correctionVector /= count; // Average out the correction vector
            transform.position += correctionVector * correctionStrength; // Apply a gentle nudge
        }
    }

    protected void OnDestroy()
    {
        dataTracker dt = FindObjectOfType<dataTracker>();
        spawner es = FindObjectOfType<spawner>();
        logicManager lm = FindAnyObjectByType<logicManager>();
        lm.addPoint(point);
        lm.addNoEnemyKilled();
        es.OnEnemyKilled();
        lm.increaseKillCount();
        Debug.Log("On destroyed called");
    }

    protected void ReturnEnemy()
    {
        spawner es = FindObjectOfType<spawner>();
        transform.position = player.position + es.relativeSpawnPos[UnityEngine.Random.Range(0, es.relativeSpawnPos.Count)].position;
    }
    public abstract void minusHealth(int damage);




}
