using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Enemy[] enemies;
    public ShootingAgent agent;
    
    public bool isEveryEnemyDead()
    {
        int deathCounter = enemies.Count(enemy => !enemy.gameObject.activeSelf);
        return deathCounter >= enemies.Length;
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
        foreach (var enemy in enemies)
        {
            enemy.gameObject.SetActive(true);
        }
    }
}
