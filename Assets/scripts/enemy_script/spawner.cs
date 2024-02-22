using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemy;
    private enemyScript enemyScript;
    private float spawnRate = 5;
    private float timer = 0;
   
    [System.Serializable]
    public class Wave
    {
        //enemyScript = enemy.GetComponent<enemyScript>();
        public string waveName;
        public List<EnemyGroup> enemyGroups;
        public int waveQuota;//Maxium enemy in one wave
        public float spawnInterval;//The interval at which enemies are spawned
        public int spawnCount;//number of spawned enemies in the wave
    }

    // Update is called once per frame
   
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
    private void Start()
    {
        player = FindObjectOfType<playerControl>().transform;
        CalculateWaveQuota();

    }

    private void FixedUpdate()
    {
        if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0)
        {
            StartCoroutine(BeginNextWave());
        }
        spawnTimer += Time.fixedDeltaTime;
        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemy();
        }
    }

    IEnumerator BeginNextWave()
    {
        yield return new WaitForSeconds(waveInterval);

        if (timer < spawnRate)
            //If there are more waves to start after the current wave, spawn the next wave
            if (currentWaveCount < waves.Count - 1)
            {
                timer += Time.deltaTime;
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
        Debug.Log(currentWaveQuota);
    }

    void SpawnEnemy()
    {
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)//If the the current wave quota is not reached and maxium enemy is not reached then spawn enemy
        {
            int enemeyIndex = UnityEngine.Random.Range(0, 2);
            Instantiate(enemy[enemeyIndex], new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            timer = UnityEngine.Random.Range(0, 3);
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)//Check if the amount of an enemy group has been reached or not
                {
                    if (enemiesAlive >= maxEnemiesAllowed)
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
    }

    //public void upgradeMob()
    //{
    //    enemyScript.health += 1;
    //    enemyScript.enemyDamage += 1;
    //    spawnRate--;
    //}

}