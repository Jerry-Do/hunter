using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Manages the logic the spawner.
/// EnemyGroup is the enemies are going to spawn 
/// in a wave
/// </summary>
public class spawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups;
        public int waveQuota;//Maxium enemy in one wave
        public float spawnInterval;//The interval at which enemies are spawned
        public int spawnCount;//number of spawned enemies in the wave
        public int enemyAlive;
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;
        public int spawnCount;
        public GameObject enemyPrefab;
    }

    public List<Wave> waves;
    public int currentWaveCount;//The index of the current wave
    Transform player;

    [Header("Spawn Attributes")]
    float spawnTimer;
    public int enemiesAlive;
    public int maxEnemiesAllowed;
    public bool maxEnemiesReached = false;
    public float waveInterval;
    [Header("SpawnPosition")]
    public List<Transform> relativeSpawnPos;

    public bool pauseFlag = false;
    private void Start()
    {
        player = FindObjectOfType<playerControl>().transform;
        CalculateWaveQuota();
       
    }

    private void FixedUpdate()
    {
       
        if(currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0 && !pauseFlag)
        {
            StartCoroutine(BeginNextWave());
        }
        spawnTimer += Time.fixedDeltaTime;
        if(spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemy();
        }
      
    }

    IEnumerator BeginNextWave()
    {
        
        yield return new WaitForSeconds(waveInterval);

        //If there are more waves to start after the current wave, spawn the next wave
        if(currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }
    void CalculateWaveQuota()//Calculate the quota of the current wave
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }
        waves[currentWaveCount].waveQuota = currentWaveQuota;
        waves[currentWaveCount].enemyAlive = currentWaveQuota;
        Debug.Log(currentWaveQuota);
    }

    void SpawnEnemy()
    {
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)//If the the current wave quota is not reached and maxium enemy is not reached then spawn enemy
        {
            foreach(var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                if(enemyGroup.spawnCount < enemyGroup.enemyCount)//Check if the amount of an enemy group has been reached or not
                {
                    if(enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }

                    Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPos[UnityEngine.Random.Range(0, relativeSpawnPos.Count)].position, Quaternion.identity);
                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                }
            }
        }
        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;

        }

    }

    public void OnEnemyKilled()
    {
        enemiesAlive--;
        waves[currentWaveCount].enemyAlive--;
        if (waves[currentWaveCount].enemyAlive == 0)
        {
            logicManager lm = FindObjectOfType<logicManager>();
            lm.spawnRandomWeapon();
            lm.setTimerFlagToTrue();
        }
    }

   public void setPauseFlag(bool flag)
   {
        pauseFlag = flag;
   }
}
