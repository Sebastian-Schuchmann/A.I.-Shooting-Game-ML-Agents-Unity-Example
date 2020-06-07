using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public int startingHealth = 100;
    public EnemyManager enemyManager;
    public float speed = 1f;

    private EnvironmentParameters EnvironmentParameters;
    private int CurrentHealth;
    private Vector3 StartPosition;

    public float randomRangeX_Pos = 0f;
    public float randomRangeX_Neg = 0f;
    public float randomRangeZ_Pos = 0f;
    public float randomRangeZ_Neg = 0f;

    public ShootingAgent Agent;
    private NavMeshAgent navAgent;
    
    private void Start()
    {
        StartPosition = transform.position;
        CurrentHealth = startingHealth;
        
        EnvironmentParameters = Academy.Instance.EnvironmentParameters;
        speed = EnvironmentParameters.GetWithDefault("zombieSpeed", 1f);
        
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = speed;

        Agent.OnEnvironmentReset += Respawn;
    }

    private void FixedUpdate()
    {
        navAgent.destination = Agent.transform.position;
        //transform.position = Vector3.MoveTowards(transform.position, Agent.transform.position, Time.fixedDeltaTime * speed);
    }

    public void GetShot(int damage, ShootingAgent shooter)
    {
        ApplyDamage(damage, shooter);
    }
    
    private void ApplyDamage(int damage, ShootingAgent shooter)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Die(shooter);
        }
    }
    
    private void Die(ShootingAgent shooter)
    {
        shooter.RegisterKill();
        
        gameObject.SetActive(false);
        enemyManager.RegisterDeath();
    }
    
    public void Respawn()
    {
        CurrentHealth = startingHealth;
        speed = EnvironmentParameters.GetWithDefault("zombieSpeed", 1f);
        navAgent.speed = speed;
        
        transform.position = new Vector3(StartPosition.x + Random.Range(randomRangeX_Neg, randomRangeX_Pos), StartPosition.y, StartPosition.z + Random.Range(randomRangeZ_Neg, randomRangeZ_Pos));
    }
}
