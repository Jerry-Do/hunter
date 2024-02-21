using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class enemy : MonoBehaviour
{
    public float despawnDistance = 20f;
    protected Transform player;
    public abstract void minusHealth(int damage);
    private void Start()
    {
        player = FindAnyObjectByType<playerControl>().transform;
    }
    private void OnDestroy()
    {
        spawner es = FindObjectOfType<spawner>();
        es.OnEnemyKilled();
    }

    protected void ReturnEnemy()
    {
        spawner es = FindObjectOfType<spawner>();
        transform.position = player.position + es.relativeSpawnPos[UnityEngine.Random.Range(0, es.relativeSpawnPos.Count)].position;
    }
}
