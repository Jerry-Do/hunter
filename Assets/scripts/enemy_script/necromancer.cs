using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class necromancer : enemy
{
    private animationController ac;
    [SerializeField] private float summonDistance;
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private List<GameObject> enemyList;
    [SerializeField] private float summoningTime;
    [SerializeField] private float cooldown;
    [SerializeField] private float maxEnemyAllowed;
    private List<int> enemiesSummoned;
    private float currentEnemyNo = 0;
    private float cooldownUse;
    private enum state
    {
        run,
        attack,
        idle, 
    }
    private state enemyState;
    private necromancer()
    {
        health = 10;
        point = 1;
        enemyDamage = 5;
    }

    // Update is called once per frame
    void Update()
    {
        ac = gameObject.GetComponent<animationController>();
        EnemyLogic();
        if (Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
        if ((player.transform.position.x - transform.position.x) <= 0)//turn the object's direction based on where the player is
        {
            transform.eulerAngles = Vector3.forward * 0;
        }
        else
        {
            transform.eulerAngles = Vector3.up * 180;
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        if(Vector2.Distance(transform.position, player.position) <= summonDistance )
        {
            enemyState = state.attack;
            
            if (cooldownUse <= 0 && currentEnemyNo < maxEnemyAllowed)
            {
                StartCoroutine(Summoning());
                cooldownUse = cooldown;
                currentEnemyNo++;
            } 
            
        }
        else
        {
            enemyState = state.run;
            
        }
        if(cooldownUse > 0)
        {
            cooldownUse -= Time.deltaTime;
            if(cooldownUse <= 0)
            {
                cooldownUse = 0;
            }
        }
       
    }
    public override void minusHealth(int damage)
    {
        health -= damage;
    }
    private void EnemyLogic()
    {
        switch (enemyState)
        {
            case state.run:
                ac.PlayStateAnimation("run");
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, player.transform.position.y), speed * Time.deltaTime);
                
                break;
            case state.attack:
                ac.PlayStateAnimation("attack");
                break;
            default:
                break;
        }

    }

    IEnumerator Summoning()
    {
        yield return new WaitForSeconds(summoningTime);
        int randomNo = UnityEngine.Random.Range(0, enemyList.Count);
        int summoned = Instantiate(enemyList[randomNo], new Vector3(transform.position.x + 5 + randomNo, transform.position.y, 0), transform.rotation).GetInstanceID();
        enemiesSummoned.Append(summoned);

    }

    public void MinusEnemyCount(int id)//testing
    {
        int i = enemiesSummoned.FindIndex(x => x == id);
        if(i > -1)
        {
            currentEnemyNo--;
            enemiesSummoned.RemoveAt(i);

        }
            
        

        
    }
}
