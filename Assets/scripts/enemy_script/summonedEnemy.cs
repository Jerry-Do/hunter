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
        lm.addPoint(point);
        lm.addNoEnemyKilled();
        es.OnEnemyKilled();
        lm.increaseKillCount();
        Debug.Log("On destroyed called");
    }

    public void setSummonerID(int id)
    {
        summonerID = id;
    }
}
