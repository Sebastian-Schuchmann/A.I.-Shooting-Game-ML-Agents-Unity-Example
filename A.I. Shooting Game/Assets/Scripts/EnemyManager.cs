using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.MLAgents;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Enemy[] enemies;
    public ShootingAgent agent;

    private int EnemyCount;
    private EnvironmentParameters EnvironmentParameters;

    private void Start()
    {
        EnvironmentParameters = Academy.Instance.EnvironmentParameters;
        EnemyCount = Mathf.FloorToInt(EnvironmentParameters.GetWithDefault("amountZombies", 3f));
        
        SetEnemiesActive();
    }

    public bool isEveryEnemyDead()
    {
        int counter = 0;
        int deathCounter = 0;
        
        foreach (var enemy in enemies)
        {
            if (counter >= EnemyCount)
                break;
            
            counter++;
            if (!enemy.isActiveAndEnabled)
                deathCounter++;
        }
        return deathCounter >= EnemyCount;
    }

    public void RegisterDeath()
    {
        if (isEveryEnemyDead())
        {
            SetEnemiesActive();
            agent.EndEpisode();
        }
    }

    public void SetEnemiesActive()
    {
        int counter = 0;
        foreach (var enemy in enemies)
        {
            if (counter >= EnemyCount)
                break;
            
            counter++;
            enemy.gameObject.SetActive(true);
        }
    }
}
