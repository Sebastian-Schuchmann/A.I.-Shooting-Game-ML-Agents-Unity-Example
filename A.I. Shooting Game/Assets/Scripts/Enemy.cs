using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public int startingHealth = 100;
    public EnemyManager enemyManager;
    
    private int CurrentHealth;
    private Vector3 StartPosition;

    public float randomRangeX_Pos = 0f;
    public float randomRangeX_Neg = 0f;
    public float randomRangeZ_Pos = 0f;
    public float randomRangeZ_Neg = 0f;

    public ShootingAgent Agent;
    
    private void Start()
    {
        StartPosition = transform.position;
        CurrentHealth = startingHealth;

        Agent.OnEnvironmentReset += Respawn;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, Agent.transform.position, Time.fixedDeltaTime);
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
        Debug.Log("I died!");
        shooter.RegisterKill();
        
        gameObject.SetActive(false);
        enemyManager.RegisterDeath();
    }
    
    public void Respawn()
    {
        CurrentHealth = startingHealth;
        transform.position = new Vector3(StartPosition.x + Random.Range(randomRangeX_Neg, randomRangeX_Pos), StartPosition.y, StartPosition.z + Random.Range(randomRangeZ_Neg, randomRangeZ_Pos));
    }
}
