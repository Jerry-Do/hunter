using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Base class for all the enemies in the game
/// </summary>
public abstract class enemy : MonoBehaviour
{
    [SerializeField] protected float despawnDistance = 20f;
    protected Transform player;
    protected int point;
    protected int health;
    protected int enemyDamage;
    public enum EnemyType { Melee, Ranged, Healer, Heavy }
    public EnemyType enemyType;


    private void Start()
    {
        player = FindAnyObjectByType<playerControl>().transform;
    }
    protected void OnDestroy()
    {
        dataTracker dt = FindObjectOfType<dataTracker>();
        spawner es = FindObjectOfType<spawner>();
        logicManager lm = FindAnyObjectByType<logicManager>();
        lm.addPoint(point);
        lm.addNoEnemyKilled();
        es.OnEnemyKilled();
        dt.increaseKillCount();
        Debug.Log("On destroyed called");
    }

    protected void ReturnEnemy()
    {
        spawner es = FindObjectOfType<spawner>();
        transform.position = player.position + es.relativeSpawnPos[UnityEngine.Random.Range(0, es.relativeSpawnPos.Count)].position;
    }
    public abstract void minusHealth(int damage);

}
