using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class summonedEnemy : enemy
{
    private int summonerID;
    protected new void OnDestroy()
    {
        dataTracker dt = FindObjectOfType<dataTracker>();
        spawner es = FindObjectOfType<spawner>();
        logicManager lm = FindObjectOfType<logicManager>();
        necromancer nm = GameObject.Find(summonerID.ToString()).GetComponent<necromancer>();
        nm.MinusEnemyCount(gameObject.GetInstanceID());
        lm.addPoint(point);
        lm.addNoEnemyKilled();
        es.OnEnemyKilled();
        lm.increaseKillCount();
      
    }

    public void setSummonerID(int id)
    {
        summonerID = id;
    }
}
