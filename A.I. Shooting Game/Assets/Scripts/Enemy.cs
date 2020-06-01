using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public int startingHealth = 100;

    private int CurrentHealth;
    private Vector3 StartPosition;

    public ShootingAgent Agent;
    
    private void Start()
    {
        StartPosition = transform.position;
        CurrentHealth = startingHealth;

        Agent.OnEnvironmentReset += Respawn;
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
        Respawn();
    }
    
    private void Respawn()
    {
        CurrentHealth = startingHealth;
        transform.position = new Vector3(StartPosition.x + Random.Range(-6f, 6f), StartPosition.y, StartPosition.z + Random.Range(-5f, 5f));
    }
}
