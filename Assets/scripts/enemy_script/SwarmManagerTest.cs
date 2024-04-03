using UnityEngine;
using System.Collections.Generic;

public class SwarmManager : MonoBehaviour
{
    public static SwarmManager Instance;

    public List<zombie> allZombies = new List<zombie>();
    public float minDistance = 1f;  // Minimum distance between each zombie
    public float adjustSpeed = 5f;  // Speed at which zombies adjust to maintain the minimum distance

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        AdjustZombiesPosition();
    }

    void AdjustZombiesPosition()
    {
        for (int i = 0; i < allZombies.Count; i++)
        {
            for (int j = i + 1; j < allZombies.Count; j++)
            {
                var distance = Vector3.Distance(allZombies[i].transform.position, allZombies[j].transform.position);
                if (distance < minDistance)
                {
                    Vector3 direction = (allZombies[i].transform.position - allZombies[j].transform.position).normalized;
                    allZombies[i].transform.position += direction * adjustSpeed * Time.deltaTime;
                    allZombies[j].transform.position -= direction * adjustSpeed * Time.deltaTime;
                }
            }
        }
    }

    public void RegisterZombie(zombie zmb)
    {
        if (!allZombies.Contains(zmb))
        {
            allZombies.Add(zmb);
        }
    }

    public void UnregisterZombie(zombie zmb)
    {
        if (allZombies.Contains(zmb))
        {
            allZombies.Remove(zmb);
        }
    }
}