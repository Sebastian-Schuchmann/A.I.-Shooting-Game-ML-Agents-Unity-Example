using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int startingHealth = 100;

    private int CurrentHealth;
    private Vector3 StartPosition; 
    
    private void Start()
    {
        StartPosition = transform.position;
        CurrentHealth = startingHealth;
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
        transform.position = StartPosition;
    }
}
